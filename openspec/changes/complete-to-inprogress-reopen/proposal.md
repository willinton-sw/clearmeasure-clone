## Why

Work orders that have been marked as Complete sometimes need to be revisited — additional work is discovered, the original fix was incomplete, or the assignee realizes a task was prematurely closed. Currently, a completed work order has no forward transitions, forcing teams to create a new work order for follow-up work on the same issue. Allowing the assignee to reopen a completed work order back to In Progress eliminates this friction and maintains the full history on a single work order.

## What Changes

- New state command (`CompleteToInProgressCommand`) enabling the transition from Complete back to In Progress
- The `CompletedDate` is cleared when the work order is reopened, reflecting that it is no longer complete
- Only the assignee of the work order can perform this transition, consistent with existing assignee-gated commands (Begin, Shelve, Complete)
- The UI automatically surfaces a "Reopen" button for the assignee when viewing a completed work order
- The state diagram and workflow documentation are updated to reflect the new transition

## Capabilities

### New Capabilities
- `complete-to-inprogress-command`: State command that transitions a work order from Complete to InProgress, authorized by the assignee, clearing CompletedDate on execution

### Modified Capabilities
- `state-command-list`: The `StateCommandList` now includes `CompleteToInProgressCommand` in its enumeration of all commands, making it available for UI rendering and command matching
- `work-order-manage-ui`: The existing `WorkOrderManage.razor` page dynamically renders valid commands as buttons — the Reopen button appears automatically for the assignee on completed work orders (no UI code changes needed)
- `acceptance-test-read-only`: The `ShouldShowSpeakButtonsOnReadOnlyWorkOrder` acceptance test must be updated because a completed work order assigned to the current user is no longer read-only (the Reopen command is now valid)

## Impact

- **New files**: `src/Core/Model/StateCommands/CompleteToInProgressCommand.cs`, unit tests, integration test, workflow sequence diagram
- **Modified files**: `StateCommandList.cs` (add command), `StateCommandListTests.cs` (update count), `arch-state-workorder.md` (update diagram), `WorkOrderSpeechTests.cs` (update read-only assertion)
- **Dependencies**: No new NuGet packages
- **Database**: No schema changes — uses existing WorkOrder table columns
- **CI/CD**: No pipeline changes
- **Deployment**: No configuration changes
