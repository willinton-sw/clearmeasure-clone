## Context

The ChurchBulletin work order system uses a state machine with five statuses: Draft, Assigned, InProgress, Complete, and Cancelled. State transitions are implemented as `IStateCommand` records inheriting from `StateCommandBase`. Each command defines a begin status, end status, authorized user check, and optional side effects. Commands are registered in `StateCommandList`, which the UI queries to determine valid actions for the current user and work order state.

Existing transitions flow linearly: Draft → Assigned → InProgress → Complete, with a Shelve (InProgress → Assigned) and Cancel (Assigned → Cancelled) as backward/exit transitions. There is currently no backward transition from Complete.

## Goals / Non-Goals

**Goals:**
- Enable the assignee to reopen a completed work order, transitioning it from Complete to InProgress
- Clear `CompletedDate` when reopening, since the work order is no longer complete
- Follow the identical pattern used by all existing state commands
- Require no changes to Core interfaces, DataAccess handlers, or UI components

**Non-Goals:**
- Adding a configurable "reopen window" (time limit after completion) — not needed at this time
- Allowing the creator (non-assignee) to reopen — only the assignee should reopen
- Adding audit trail entries for the reopen action beyond what `ChangeStatus` already records
- Adding a new status (e.g., "Reopened") — reuse of InProgress is intentional

## Decisions

### Decision 1: Reuse InProgress status rather than adding a new "Reopened" status

**Rationale:** The work order is being resumed — the assignee is picking up work again. InProgress accurately describes the state. Adding a new status would require database migration, UI changes, and EF Core mapping updates for no semantic benefit.

**Alternatives considered:**
- New "Reopened" status: Adds complexity (DB migration, status enum, UI) without value. The transition history captures that it was reopened.

### Decision 2: Clear CompletedDate on reopen

**Rationale:** `CompletedDate` represents when the work order was finished. Once reopened, it is no longer finished. When completed again, a new `CompletedDate` will be set. This is consistent with the InProgressToCompleteCommand setting `CompletedDate`.

**Alternatives considered:**
- Keep CompletedDate: Would be misleading — the work order shows a completion date while being actively worked on.

### Decision 3: Only the assignee can reopen

**Rationale:** Consistent with all other assignee-phase transitions (Begin, Shelve, Complete). The person doing the work decides whether it needs to be reopened.

### Decision 4: Transition verb "Reopen" / "Reopened"

**Rationale:** Clear and unambiguous. The UI button will display "Reopen" which communicates the intent to the user. "Resume" was considered but implies the work was paused rather than marked complete.

## Risks / Trade-offs

- **[Read-only behavior change]** Completed work orders previously appeared read-only to the assignee. Now the assignee sees a "Reopen" action button. The existing acceptance test `ShouldShowSpeakButtonsOnReadOnlyWorkOrder` must be updated to account for this. → Mitigation: Remove the `ReadOnlyMessage` assertion from that test since the completed work order is no longer read-only for the assignee.
- **[Repeated completion]** A work order could be completed and reopened multiple times, with only the latest `CompletedDate` preserved. → Acceptable: The transition history (if audited) captures the full lifecycle. The `CompletedDate` field represents the current completion, not the first.

## Open Questions

- None at this time. The feature follows established patterns exactly.
