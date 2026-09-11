# FA OEE - Optimized Extreme

Source baseline:

`C:\Users\kidakarn_i\Desktop\ดฟ\FA_OEE_SQLITE_OK-main`

Optimized copy:

`C:\Users\kidakarn_i\Desktop\ดฟ\FA_OEE_SQLITE_OK-main_OPTIMIZED_EXTREME`

## Safety rules

- The baseline directory is not edited.
- Production URLs, API parameters, SQL text, SQLite schema, counters, cavity values,
  transfer flags, Close Lot rules, Serial Port settings and DAQ behavior are unchanged.
- The application is not launched during automated verification, so it cannot contact
  production services or write production/local database records.
- All seven database files match the baseline SHA-256 hashes after the optimization pass.

## Implemented optimizations

- Added true asynchronous GET requests with bounded timeout and request abort.
- Replaced 11 `Task.Run` wrappers around synchronous GET requests.
- Cached successful SQLite WAL initialization per connection string.
- Added a five-second SQLite command timeout to the central query paths.
- Closed selected SQL Server connections together with their returned readers by using
  `CommandBehavior.CloseConnection`; SQL commands and returned rows are unchanged.
- Added ownership-based cleanup for generated QR bitmaps to reduce GDI handle growth.
- Disposed QRCoder generator/data/code objects after each generated bitmap.
- Removed an unresolved and unused `System.Text.Json` reference that targeted a framework
  newer than .NET Framework 4.6.1.

## Verification

- Debug rebuild: passed, 0 errors.
- Release rebuild: passed, 0 errors.
- Local asynchronous GET response test: passed (`ASYNC_OK`).
- Local timeout test: passed; returned `Nothing` in approximately 279 ms for a 250 ms timeout.
- Database hash verification: 7 files checked, 0 mismatches.

## Intentionally deferred

The remaining large SQL/SQLite refactor, package removal, architecture change, Timer
coalescing and installer cleanup require a production-like test database and hardware
regression pass. They are not applied blindly because they can affect production timing,
printing, DAQ or transfer behavior even when they compile successfully.
