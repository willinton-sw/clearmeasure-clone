## 1. State Command Implementation

- [x] 1.1 Create `src/Core/Model/StateCommands/CompleteToAssignedCommand.cs` implementing `StateCommandBase` with begin=Complete, end=Assigned, creator-only authorization, and `CompletedDate` clearing
- [x] 1.2 Register `CompleteToAssignedCommand` in `src/Core/Services/Impl/StateCommandList.GetAllStateCommands()`

## 2. Unit Tests

- [x] 2.1 Create `src/UnitTests/Core/Model/StateCommands/CompleteToAssignedCommandTests.cs` with tests for: wrong status validation, wrong employee validation, valid state check, state transition execution, and CompletedDate clearing
- [x] 2.2 Update `StateCommandListTests.ShouldReturnAllStateCommandsInCorrectOrder` to expect 7 commands and assert `CompleteToAssignedCommand` at index 6

## 3. Integration Tests

- [x] 3.1 Create `src/IntegrationTests/DataAccess/Handlers/StateCommandHandlerForReassignTests.cs` testing persistence of Complete → Assigned transition through the `StateCommandHandler`

## 4. Documentation

- [x] 4.1 Update `arch/arch-state-workorder.md` state diagram to include `Complete --> Assigned : CompleteToAssignedCommand`

## 5. UI Verification

- [x] 5.1 Verify that `WorkOrderManage.razor` automatically renders the "Reassign" button for completed work orders viewed by their creator (no code changes needed — `ValidCommands` handles this)
