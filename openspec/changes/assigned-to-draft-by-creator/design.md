## Context

The ChurchBulletin work order system uses a state machine pattern where each status transition is encapsulated in a `StateCommandBase` record. Each command defines its begin/end status, permission check (`UserCanExecute`), and optional side effects in `Execute()`. The `StateCommandList` class aggregates all commands and filters to valid ones based on the current work order status and user. The Blazor UI dynamically renders action buttons from the valid command list.

Existing transitions from Assigned: `AssignedToInProgressCommand` (by assignee), `AssignedToCancelledCommand` (by creator). The new `AssignedToDraftCommand` follows the same pattern as `AssignedToCancelledCommand` — creator-only, clears assignee data — but transitions to Draft instead of Cancelled.

## Goals / Non-Goals

**Goals:**
- Allow the creator to move an Assigned work order back to Draft status
- Clear Assignee and AssignedDate when transitioning back to Draft
- Follow the existing state command pattern exactly (inherit `StateCommandBase`, implement all abstract members)
- Register in `StateCommandList` so the UI picks it up automatically

**Non-Goals:**
- Allowing the assignee to unassign themselves (only the creator can unassign)
- Adding new UI components or pages (existing dynamic rendering handles it)
- Modifying the `CanReassign()` method on `WorkOrder` (it already returns `true` for Draft status, which is the end state)

## Decisions

### Decision 1: Transition verb is "Unassign" / "Unassigned"

**Rationale:** The verb clearly communicates the action — reversing an assignment. It parallels the existing "Assign" verb on `DraftToAssignedCommand`. The UI button will display "Unassign" which is intuitive for users.

**Alternatives considered:**
- "Return to Draft": Too verbose for a button label
- "Revert": Ambiguous — could imply reverting changes, not just status

### Decision 2: Clear Assignee and AssignedDate on execution

**Rationale:** Moving back to Draft should fully reset the assignment state, matching the behavior of `AssignedToCancelledCommand`. The work order returns to the same state it was in before assignment, allowing the creator to reassign to a different person.

### Decision 3: Only the Creator can execute

**Rationale:** Consistent with `AssignedToCancelledCommand` and `DraftToAssignedCommand` — the creator controls assignment lifecycle. The assignee has their own transitions (`AssignedToInProgressCommand`).

## Risks / Trade-offs

- **[State consistency]** After unassigning, the work order is back in Draft with no assignee. The creator must re-assign before the work order can progress. → Acceptable: this is the intended behavior and matches the initial Draft state.
- **[No notification]** The assignee is not notified when they are unassigned. → Acceptable for MVP: notification infrastructure does not exist for other transitions either.
