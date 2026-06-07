Architecture Decision Record 2: Quick Notes

# Context
I don't want to write a fully formatted ADR for every small decision.

# Decision
I'll write a short entry in here for small decisions that don't need a full ADR, but I want to keep track of them.

# Consequences

**Decisions:**

## OperationResult over Task/Task\<T\> for DataRepository

`LoadAsync`/`SaveAsync` return `OperationResult`/`OperationResult<Schema>` instead of bare `Task`/`Task<Schema>`.

Reason: call sites (ViewModels) should not need try/catch blocks for expected failure paths like missing files or unsupported formats. 
The result type makes the failure branch structurally visible and hard to ignore. 
`Exception?` is preserved on the result so stack traces are not silently lost when debugging. 
Trade-off acknowledged: slightly more ceremony than idiomatic .NET Tasks; revisit if the pattern spreads beyond infrastructure I/O.
