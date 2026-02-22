# NummyUi — Improvement Plan

## 1. Security

### 1.1 In-memory session has no persistence and is easily bypassed
**File:** `Session/UserSession.cs`
**Problem:** `UserSession` is a plain in-memory Scoped service. In Blazor Server, circuits are tied to the SignalR connection. A page refresh or server restart causes the session to be lost, requiring re-login. More critically, there are no route guards — any unauthenticated user who knows the URL can navigate directly to `/application`, `/team`, etc.
**Fix:**
- Implement proper ASP.NET Core authentication with cookies (`CookieAuthentication`) or JWT stored in an `HttpOnly` cookie.
- Add a `<AuthorizeView>` component or `[Authorize]` attribute to protected pages.
- Alternatively, keep the in-memory session but add a Blazor `AuthenticationStateProvider` that gates route rendering.

### 1.2 `_isLoading` not reset on exception during login
**File:** `Pages/User/Login/Index.razor.cs:24-41`
**Problem:** If `UserService.Login` or `UserService.Get` throws an exception, the `catch` block is absent. `_isLoading` stays `true`, leaving the login button permanently in a loading/disabled state for the duration of the Blazor circuit.
**Fix:** Wrap the login logic in a `try/catch/finally` and set `_isLoading = false` in the `finally` block:
```csharp
try { ... }
catch (Exception ex) { await Message.Error($"Unexpected error: {ex.Message}"); }
finally { _isLoading = false; }
```

---

## 2. Reliability

### 2.1 Widespread use of null-forgiveness operator without proper handling
**Files:** `Services/ApplicationService.cs`, `Services/LogService.cs`, `Services/UserService.cs`, etc.
**Problem:** Multiple service methods return `result!` after deserializing an HTTP response. If the API returns `null` (or an empty body), these sites throw a `NullReferenceException` which propagates as an unhandled circuit exception, crashing the Blazor component.
**Fix:** Null-check after deserialization and throw a descriptive exception or return a default, depending on the context. For list endpoints, return an empty collection rather than null.

### 2.2 `UserService.Login` does not call `EnsureSuccessStatusCode()`
**File:** `Services/UserService.cs:34-37`
**Problem:** Unlike every other service, `Login` reads the response body without first checking the HTTP status. A `500` from the API will still try to deserialize the error body as `LoginResponseDto`, silently returning `null!`.
**Fix:** Add `response.EnsureSuccessStatusCode()` before `ReadFromJsonAsync`, or handle non-success status codes explicitly.

### 2.3 No global error boundary for Blazor components
**Problem:** Unhandled exceptions inside `OnInitializedAsync` or event handlers (other than the few wrapped in `try/catch`) crash the Blazor circuit and show a generic browser error.
**Fix:** Wrap the root layout or individual page components with `<ErrorBoundary>` (available since .NET 6) to display a user-friendly fallback UI instead of crashing.

### 2.4 Missing validation of environment variables at startup
**File:** `Program.cs:39-42`
**Problem:** `NUMMY_API_HOST` and `NUMMY_API_PORT` are used without null checks. A missing variable produces a malformed URI, and `new Uri(baseUrl)` throws at startup with an opaque message.
**Fix:** Validate both variables before use and throw a descriptive `InvalidOperationException`.

---

## 3. Performance

### 3.1 Stack types loaded on every modal open
**File:** `Pages/Application/Index.razor.cs:51-56`
**Problem:** `LoadStackTypes()` calls the API every time the Add/Edit modal is opened. Stack types are seeded static data that almost never change.
**Fix:** Load stack types once in `OnInitializedAsync` alongside applications, and cache them in the component field. A page refresh is sufficient to pick up any rare changes.

### 3.2 No caching of static API responses
**Problem:** Common reference data (stack types, user list) is re-fetched on every component initialization with no HTTP caching headers or client-side cache.
**Fix:** Consider adding a lightweight in-memory cache service (`IMemoryCache`) in the DI container for frequently read, rarely changed data.

---

## 4. Maintainability

### 4.1 Interface and implementation in the same file
**Files:** All files in `Services/` (e.g., `ApplicationService.cs`, `UserService.cs`, `LogService.cs`)
**Problem:** Collocating interface and implementation in one file violates single-responsibility and makes the files unnecessarily long. Finding the contract vs. the implementation requires scrolling.
**Fix:** Move each interface to its own file under `Services/Abstract/` (mirroring the NummyApi pattern).

### 4.2 `NummyConstants.GetCodeLogsByTraceIdentifierUrl` duplicates `GetCodeLogsUrl`
**File:** `Utils/NummyConstants.cs:13,15`
**Problem:** Both constants have the value `"api/log/get/code"`. The ambiguity means the distinction between the two is unclear; if one changes, the other will silently diverge.
**Fix:** Rename `GetCodeLogsByTraceIdentifierUrl` to make it clear it is the same base path with a different suffix appended at call site, or consolidate into a single constant.

### 4.3 `AddButton` component is incomplete (no `razor.cs` for `MigrationPanel`)
**File:** `Components/MigrationPanel.razor.cs`
**Problem:** `MigrationPanel` has a `.razor` and a `.razor.cs` file suggesting active development, but the pattern `_isTeamEditOrAddModalVisible` (naming mismatch with actual component purpose) appears in `Application/Index.razor.cs` indicating copy-paste naming issues.
**Fix:** Audit component naming for consistency — variable names should reflect the component context they belong to.

### 4.4 Too many models that appear to be scaffold leftovers
**Files:** `Models/ActivityGroup.cs`, `Models/ActivityProject.cs`, `Models/ActivityUser.cs`, `Models/AdvancedOperation.cs`, `Models/AdvancedProfileData.cs`, `Models/BasicGood.cs`, `Models/BasicProfileDataType.cs`, `Models/BasicProgress.cs`, `Models/OfflineChartDataItem.cs`, `Models/OfflineDataItem.cs`, `Models/RadarDataItem.cs`, `Models/SearchDataItem.cs`, `Models/UserLiteItem.cs`, etc.
**Problem:** Many model classes appear to be leftover scaffolding from the AntDesign Pro template and are no longer referenced by any active page or component.
**Fix:** Audit all models against actual usage. Remove unused models to reduce dead code and confusion.

### 4.5 Large number of excluded `.razor` files in the project
**File:** `NummyUi.csproj` — 30+ `_ContentIncludedByDefault Remove` entries
**Problem:** Dozens of scaffolded Razor pages are excluded via project file directives, rather than being deleted. They add noise to the project and could be accidentally un-excluded.
**Fix:** Delete unused scaffold files from disk and remove the exclusion entries from the `.csproj`.

---

## 5. User Experience / Stability

### 5.1 No loading state on `Register` page
**File:** `Pages/User/Register/Index.razor.cs`
**Problem:** Unlike the Login page which has `_isLoading`, the Register page (if implemented) does not disable the submit button during the async call, allowing multiple simultaneous submissions.
**Fix:** Add an `_isLoading` guard and disable the submit button while the registration request is in flight.

### 5.2 HTTP service calls not cancellable
**Problem:** All `HttpClient` calls in UI services use the default overload with no `CancellationToken`. When a user navigates away before a response arrives, the pending request continues unnecessarily.
**Fix:** Accept and forward `CancellationToken` through service methods, and pass `ComponentBase.DisposeToken` or a linked `CancellationTokenSource` from components.
