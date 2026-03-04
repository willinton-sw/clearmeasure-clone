## Why

Currently, once a work order reaches the **Complete** status, there is no way to revert it. If the creator determines the work was not performed satisfactorily or additional work is needed, the only option is to create an entirely new work order. Allowing the creator to move a completed work order back to **Assigned** enables a correction workflow without duplicating work orders.

## What Changes

- New state command `CompleteToAssignedCommand` implementing the Complete → Assigned transition
- Only the **Creator** of the work order can execute this transition
- The `CompletedDate` is cleared when the work order returns to Assigned
- The command is registered in `StateCommandList` so the UI automatically renders a **Reassign** button
- Architecture state diagram updated to reflect the new transition

## Capabilities

### New Capabilities
- `complete-to-assigned-command`: State command enabling Complete → Assigned transition by the work order creator

### Modified Capabilities
- `StateCommandList`: Extended to include the new `CompleteToAssignedCommand`
- `arch-state-workorder.md`: Updated state diagram with the new transition arrow

## Impact

- **Core project**: One new file (`CompleteToAssignedCommand.cs`) in `src/Core/Model/StateCommands/`
- **StateCommandList**: One additional line registering the new command
- **Database**: No schema changes — reuses existing status codes
- **UI**: No UI code changes — the `WorkOrderManage.razor` component already renders buttons for all valid commands via `ValidCommands`
- **Tests**: New unit tests and integration test following existing patterns
