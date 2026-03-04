## Why

When a work order is in the **Assigned** state, the creator currently can only cancel it. There is no way to move it back to **Draft** for further editing or reassignment to a different person. This forces the creator to cancel and recreate the work order, losing context and history. Allowing an "Unassign" transition back to Draft gives the creator flexibility to correct assignment mistakes or hold off on a work order without cancellation.

## What Changes

- New state command `AssignedToDraftCommand` enabling the Assigned → Draft transition
- Only the **Creator** of the work order can execute this command
- Executing the command clears the Assignee and AssignedDate fields
- The command is registered in `StateCommandList` so it appears as a valid action in the UI
- The UI automatically renders an "Unassign" button for the creator when viewing an Assigned work order (no Blazor changes needed — the existing dynamic button rendering handles it)

## Capabilities

### New Capabilities
- `assigned-to-draft-command`: State command transitioning a work order from Assigned back to Draft, executable only by the creator
- `state-command-list-registration`: Registration of the new command in the centralized command list
- `ui-unassign-button`: Dynamic rendering of the "Unassign" action button in the work order management page

### Modified Capabilities
<!-- No existing capabilities are modified. The new command is additive. -->

## Impact

- **New file**: `src/Core/Model/StateCommands/AssignedToDraftCommand.cs`
- **Modified file**: `src/Core/Services/Impl/StateCommandList.cs` (one line added to register the command)
- **Architecture**: Updated state diagram in `arch/arch-state-workorder.md`
- **Database**: No schema changes — uses existing WorkOrder entity fields
- **Dependencies**: No new NuGet packages
- **UI**: No Blazor file changes — existing dynamic command button rendering in `WorkOrderManage.razor` automatically shows the "Unassign" button
