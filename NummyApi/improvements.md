# NummyApi — Improvement Plan

## 1. Critical Bugs

### 1.1 Double-mapping in `CodeLogService.Get(string traceIdentifier)`
**File:** `Services/Concrete/CodeLogService.cs:97-98`
**Problem:** `mapped` is already an `IEnumerable<CodeLogToListDto>`, yet it is passed through `mapper.Map` a second time, producing a mapping from DTO back to DTO. The second call is redundant and could silently produce wrong results if AutoMapper ever has a self-map configured.
**Fix:** Return `mapped` directly, removing the second `mapper.Map` call.

```csharp
// Before (wrong):
var mapped = mapper.Map<IEnumerable<CodeLogToListDto>>(logs);
return mapper.Map<IEnumerable<CodeLogToListDto>>(mapped); // maps DTO -> DTO

// After (correct):
var mapped = mapper.Map<IEnumerable<CodeLogToListDto>>(logs);
return mapped;
```

---

### 1.2 Deadlock in `HealthCheckerService` — double semaphore acquire
> This bug lives in **NummyWorker** but is caused by the service design, so it is noted here too (see NummyWorker improvements for the full fix).
**File (Worker):** `Services/HealthCheckerService.cs`
`semaphore.WaitAsync` is called in the `foreach` loop (line 81) **and again** inside `CheckSingleUrl` (line 108). When `MaxConcurrency` is reached, both acquire attempts block each other causing a deadlock. Remove the acquire from one location.

---

### 1.3 Wrong response checked after bulk-update PUT
> Also described in NummyWorker, but the `ApplicationController.UpdateIsHealthy` endpoint should return an appropriate status to help detect this faster. Currently the endpoint returns `Ok()` unconditionally even when zero records matched, making it impossible to distinguish success from a no-op.
**Fix:** Return `204 NoContent` when the list is empty rather than `200 OK` with no body, or add a count to the response.

---

## 3. Performance

### 3.1 `StatisticalService` issues 8 sequential DB round-trips
**File:** `Services/Concrete/StatisticalService.cs:11-26`
**Problem:** `GetTotalCountsAsync` calls 8 independent `CountAsync()` queries one after another. On a busy database this adds up to significant latency.
**Fix:** Run independent queries concurrently with `Task.WhenAll`, or consolidate into fewer queries using SQL `FILTER` clauses via raw SQL / a stored procedure.

```csharp
var total = dataContext.RequestLogs.CountAsync();
var today = CountRequestsByDateAsync(now);
// ...
await Task.WhenAll(total, today, ...);
```

### 3.2 Date-component comparisons prevent index use
**File:** `Services/Concrete/StatisticalService.cs:29-35`, `37-45`, `93-101`, `110-116`
**Problem:** Filtering by `.Year`, `.Month`, `.Day` component properties cannot use a B-tree index on `CreatedAt`. EF Core translates them with `EXTRACT(YEAR …)` calls which block index scans.
**Fix:** Use range comparisons that are index-friendly:
```csharp
var start = date.Date;
var end = start.AddDays(1);
.Where(l => l.CreatedAt >= start && l.CreatedAt < end)
```

### 3.3 `LazyLoadingProxies` enabled globally
**File:** `DataContext/NummyDataContext.cs:27`
**Problem:** Lazy loading can cause N+1 queries, especially in `TeamService` where teams with many users/applications are fetched in a list. This is partly mitigated by explicit `Include` calls, but the proxy overhead and risk of accidental lazy loads remain.
**Fix:** Remove `UseLazyLoadingProxies()` and rely solely on explicit `Include` calls. Add integration tests to catch N+1 regressions.

### 3.4 `TeamService.AddUsersToTeam` / `AddApplicationsToTeam` — individual `AddAsync` in loop
**File:** `Services/Concrete/TeamService.cs:102-114`, `116-128`
**Problem:** Each entity is added individually in a loop, resulting in one `INSERT` per row on `SaveChangesAsync`.
**Fix:** Build the list first and call `AddRangeAsync` once:
```csharp
var teamUsers = userIds.Select(uid => new TeamUser { TeamId = teamId, UserId = uid });
await dataContext.TeamUsers.AddRangeAsync(teamUsers);
```

### 3.5 `RequestLogService.Add` saves twice
**File:** `Services/Concrete/RequestLogService.cs:33,43`
**Problem:** `SaveChangesAsync` is called once for the `RequestLog` and again for the `Header` records, doubling the number of database round-trips for each inbound request log.
**Fix:** Add all entities to the context first, then call `SaveChangesAsync` once at the end.

### 3.6 `CodeLogService.Delete` loads entities before deleting
**File:** `Services/Concrete/CodeLogService.cs:78-84`
**Problem:** All matching logs are loaded into memory before being removed. For large batches this wastes memory and an extra round-trip.
**Fix:** Use `ExecuteDeleteAsync` (already used in `RequestLogService.Delete`) for a direct `DELETE … WHERE id IN (…)`.

---

## 4. Maintainability

### 4.1 Package versions targeting net8 while project targets net9
**File:** `NummyApi.csproj`
**Problem:** `Microsoft.EntityFrameworkCore`, `Npgsql.EntityFrameworkCore.PostgreSQL`, `Microsoft.EntityFrameworkCore.Proxies`, and `Microsoft.AspNetCore.OpenApi` are pinned to version `8.0.0` while `<TargetFramework>` is `net9.0`. This creates a version mismatch that may produce runtime warnings or subtle behavior differences.
**Fix:** Upgrade all Microsoft.EntityFrameworkCore and related packages to `9.x.x`.

### 4.2 `AutoMapper.Extensions.Microsoft.DependencyInjection` is deprecated
**File:** `NummyApi.csproj`
**Problem:** This package was merged into the main `AutoMapper` package starting from AutoMapper v13. Using the deprecated extension package may cause DI registration issues in future upgrades.
**Fix:** Remove `AutoMapper.Extensions.Microsoft.DependencyInjection` and use `AutoMapper` ≥ 13 with `services.AddAutoMapper(...)` directly. Also consider migrating to `Mapster` for better performance.

### 4.3 `LogController` and `StatisticalController` inherit from `Controller` instead of `ControllerBase`
**File:** `Controllers/LogController.cs:12`, `Controllers/StatisticalController.cs:8`
**Problem:** `Controller` adds MVC View support (Razor views, `ViewBag`, etc.) which is unnecessary overhead for pure JSON API controllers.
**Fix:** Change the base class to `ControllerBase`.

### 4.4 Missing `[ApiController]` attribute on `LogController` and `StatisticalController`
**File:** `Controllers/LogController.cs`, `Controllers/StatisticalController.cs`
**Problem:** Without `[ApiController]`, automatic model validation, binding source inference, and ProblemDetails error responses are not active.
**Fix:** Add `[ApiController]` to both controllers.

### 4.5 Inline database seeding in `Program.cs`
**File:** `Program.cs:51-88`
**Problem:** The seeding logic (migration check + admin user creation) is embedded directly in `Program.cs`, making it hard to test and reuse.
**Fix:** Extract into a `DbInitializer` or `DataSeed` static class with a clearly named `InitializeAsync(IServiceProvider services)` method.

### 4.6 No global exception handling middleware
**Problem:** Unhandled exceptions from services surface as unformatted 500 responses. Stacktraces can leak in development.
**Fix:** Add `app.UseExceptionHandler(...)` or a custom `IMiddleware` that returns a structured `ProblemDetails` response for all unhandled exceptions.

---

## 5. Reliability

### 5.1 `RequestLogService.Add` and `CodeLogService.Add` throw generic `Exception`
**File:** `Services/Concrete/RequestLogService.cs:26-28`, `Services/Concrete/CodeLogService.cs:17-19`
**Problem:** Throwing `new Exception(...)` is unspecific and difficult to catch selectively. Callers cannot distinguish this domain error from infrastructure errors.
**Fix:** Define a `NummyDomainException` (or `ApplicationNotFoundException`) and throw that instead. Catch it in the controller or middleware to return a `404 NotFound` or `422 UnprocessableEntity` with a proper body.

### 5.2 Missing `CancellationToken` propagation in service methods
**Problem:** Controller actions receive a `CancellationToken` from ASP.NET Core (via `HttpContext.RequestAborted`) but none of the service methods accept or forward it. If the client disconnects, the DB query continues unnecessarily.
**Fix:** Add `CancellationToken cancellationToken = default` to all service interface methods and forward it to EF Core and `HttpClient` calls.

### 5.3 No health check endpoint for the API itself
**Problem:** There is no `/health` or `/healthz` endpoint, making it impossible for container orchestrators (Docker, Kubernetes) to determine whether the API is ready to accept traffic.
**Fix:** Add `builder.Services.AddHealthChecks().AddNpgsql(connectionString)` and `app.MapHealthChecks("/health")`.

### 5.4 `app.UseHttpsRedirection()` in a container-only API
**File:** `Program.cs:94`
**Problem:** The API runs on `http://*:8082` and the HTTPS port is commented out. `UseHttpsRedirection` will never trigger but may cause confusion if the port is later enabled.
**Fix:** Remove `UseHttpsRedirection()` or add the HTTPS listener back and configure certificates correctly.

### 5.5 `ResponseLogService.Get` uses two independent queries without a transaction
**File:** `Services/Concrete/ResponseLogService.cs:77-86`
**Problem:** Request and Response logs are fetched in two separate queries. A response log written between the two reads would be missed or an inconsistent view could be returned.
**Fix:** Join the two tables in a single query, or wrap both reads in a `IDbContextTransaction` with `IsolationLevel.ReadCommitted`.
