# NummyWorker — Improvement Plan

## 1. Critical Bugs

### 1.1 Deadlock — semaphore acquired twice per URL
**File:** `Services/HealthCheckerService.cs:81` and `:108`
**Problem:** `semaphore.WaitAsync(cancellationToken)` is called in the `foreach` loop before adding the task, **and** called again at the top of `CheckSingleUrl`. When `MaxConcurrency` (10) tasks are all waiting inside `CheckSingleUrl` for the semaphore, the outer loop is also blocked trying to acquire it. The worker deadlocks once the number of URLs exceeds `MaxConcurrency`.

**Fix:** Remove the `semaphore.WaitAsync` call from the `foreach` loop — acquire is the sole responsibility of `CheckSingleUrl`:

```csharp
// In CheckAllUrls — remove the WaitAsync before Add:
foreach (var item in urlsToCheck!)
{
    // DO NOT await semaphore here
    tasks.Add(CheckSingleUrl(item, client, semaphore, results, cancellationToken));
}
```

### 1.2 Wrong response object checked after bulk-update PUT
**File:** `Services/HealthCheckerService.cs:96`
**Problem:** After sending `request2` (the PUT to update health statuses), the code calls `response.EnsureSuccessStatusCode()` instead of `response2.EnsureSuccessStatusCode()`. This means a failed PUT is silently ignored, and the previous GET success is re-evaluated (which is always fine). Bad health status updates are never detected.

```csharp
// Before (bug):
var response2 = await client.SendAsync(request2, cancellationToken);
response.EnsureSuccessStatusCode(); // wrong: checks GET response

// After (fix):
var response2 = await client.SendAsync(request2, cancellationToken);
response2.EnsureSuccessStatusCode(); // correct: checks PUT response
```

---

## 2. Reliability

### 2.1 No HTTP timeout configured for the named `HttpClient`
**File:** `Program.cs:16`
**Problem:** The `Timeout` for the named client is commented out. The default `HttpClient` timeout is 100 seconds. If the Nummy API is unresponsive, every cycle of `HealthCheckerService` blocks for up to 100 seconds, stalling the entire background worker.
**Fix:** Set a sensible timeout (e.g., 10 seconds) on the named client:
```csharp
config.Timeout = TimeSpan.FromSeconds(10);
```

### 2.3 Empty URL list still sends a PUT to the API
**File:** `Services/HealthCheckerService.cs:68-98`
**Problem:** When `urlsToCheck` is empty, the worker logs an info message but continues to build an empty results list and sends a PUT request with `[]` as the body. This wastes a network round-trip and generates unnecessary API activity.
**Fix:** Return early if `urlsToCheck` is null or empty:
```csharp
if (urlsToCheck == null || urlsToCheck.Count == 0)
{
    logger.LogInformation("No URLs to check. Skipping cycle.");
    return;
}
```

---

## 3. Performance

### 3.1 A new `HttpClient` is created from the factory every cycle
**File:** `Services/HealthCheckerService.cs:54`
**Problem:** `httpClientFactory.CreateClient(...)` is called inside `CheckAllUrls`, which runs every 7 seconds. While `IHttpClientFactory` manages the underlying `HttpMessageHandler` lifetimes, creating a client object on every invocation is wasteful.
**Fix:** Create the client once in `ExecuteAsync` (or inject it via a typed client) and reuse it across cycles.

### 3.2 `Task` list grows unbounded before `WhenAll`
**File:** `Services/HealthCheckerService.cs:77-86`
**Problem:** All tasks are started and accumulated in a `List<Task>` before awaiting. For a very large number of URLs the list may be large before being awaited, holding references to all results.
**Fix:** This is acceptable at current scale, but consider using `Parallel.ForEachAsync` with a `MaxDegreeOfParallelism` for cleaner flow control when the URL count grows.

---

## 4. Maintainability

### 4.1 `NummyConstants` should be a `static` class
**File:** `NummyConstants.cs`
**Problem:** `NummyConstants` is an instance class with only constants. It can be instantiated, which is meaningless.
**Fix:** Add the `static` modifier.

### 4.2 Hard-coded check interval and concurrency as `static readonly` fields
**File:** `Services/HealthCheckerService.cs:14,17,20`
**Problem:** `Interval` (7 s), `RequestTimeout` (5 s), and `MaxConcurrency` (10) are baked into the class with no way to configure them at runtime without recompiling.
**Fix:** Bind these from `appsettings.json` or environment variables using the options pattern (`IOptions<HealthCheckerOptions>`).

### 4.3 No health check endpoint for the worker itself
**Problem:** The worker starts an ASP.NET Core web host (`WebApplication.CreateBuilder`) but exposes no endpoints. Container orchestrators cannot distinguish a healthy running worker from a crashed one.
**Fix:** Add `app.MapHealthChecks("/health")` so orchestrators (Docker healthcheck, Kubernetes liveness probe) can verify the worker process is alive.

### 4.4 Separate `HttpRequestMessage` objects for each request
**File:** `Services/HealthCheckerService.cs:56-92`
**Problem:** `request` and `request2` are manually constructed `HttpRequestMessage` objects. Using `client.GetFromJsonAsync` and `client.PutAsJsonAsync` extension methods would reduce boilerplate significantly.

---
