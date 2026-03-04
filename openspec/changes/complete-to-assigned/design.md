## Context

The ChurchBulletin system uses a state command pattern for work order status transitions. Each transition is a record inheriting from `StateCommandBase` that defines begin/end statuses, authorization rules, and optional side effects. The `StateCommandList` enumerates all commands, and the UI renders buttons for valid transitions automatically.

Existing transitions: Draft → Draft (Save), Draft → Assigned, Assigned → InProgress, InProgress → Assigned (Shelve), InProgress → Complete, Assigned → Cancelled. There is currently no transition out of the Complete state.

## Goals / Non-Goals

**Goals:**
- Allow the creator of a work order to send a completed work order back to Assigned status
- Clear the `CompletedDate` when reverting to Assigned
- Follow the existing `StateCommandBase` pattern exactly
- Provide unit and integration test coverage

**Non-Goals:**
- Allowing the assignee to perform this transition (only the creator)
- Adding new UI components (the existing `ValidCommands` rendering handles this)
- Changing any other state transitions
- Adding notifications or events for this transition (can be added later)

## Decisions

### Decision 1: Only the Creator can execute this transition

**Rationale:** The creator is the person who requested the work. They are the appropriate authority to determine whether completed work meets expectations. This mirrors the `DraftToAssignedCommand` and `AssignedToCancelledCommand` which also restrict execution to the creator.

**Alternatives considered:**
- Allowing the assignee: Would blur the authorization model since the assignee already has `InProgressToAssignedCommand` (Shelve) for their own corrections
- Allowing any user with a specific role: Over-engineered for this use case

### Decision 2: Clear CompletedDate on transition

**Rationale:** The work order is no longer complete, so the `CompletedDate` should be nulled. This is the inverse of `InProgressToCompleteCommand` which sets `CompletedDate`. The `AssignedDate` is preserved since the work order retains its assignee.

### Decision 3: Use "Reassign" as the verb label

**Rationale:** The button label should clearly communicate the action. "Reassign" conveys that the work order is being sent back for assignment/work. This distinguishes it from "Shelve" (InProgress → Assigned by the assignee).

## Risks / Trade-offs

- **[Completed work orders reappearing]** Work orders that were considered done will reappear in the Assigned queue. → Mitigation: Only the creator can trigger this, providing a natural gatekeeping mechanism.
- **[CompletedDate loss]** The original completion date is cleared. → Mitigation: This is intentional — the work order is no longer complete. Audit trail can be added later if needed.

## Open Questions

- None at this time.
