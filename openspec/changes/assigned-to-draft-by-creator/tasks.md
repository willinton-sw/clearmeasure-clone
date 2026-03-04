## 1. State Command Implementation

- [x] 1.1 Create `src/Core/Model/StateCommands/AssignedToDraftCommand.cs` inheriting from `StateCommandBase`
- [x] 1.2 Implement `GetBeginStatus()` returning `WorkOrderStatus.Assigned`
- [x] 1.3 Implement `GetEndStatus()` returning `WorkOrderStatus.Draft`
- [x] 1.4 Implement `UserCanExecute()` checking `currentUser == WorkOrder.Creator`
- [x] 1.5 Override `Execute()` to clear `WorkOrder.Assignee` and `WorkOrder.AssignedDate` before calling `base.Execute()`
- [x] 1.6 Set `TransitionVerbPresentTense` to "Unassign" and `TransitionVerbPastTense` to "Unassigned"

## 2. Command Registration

- [x] 2.1 Add `new AssignedToDraftCommand(workOrder, currentUser)` to `StateCommandList.GetAllStateCommands()`

## 3. Unit Tests

- [x] 3.1 Create `src/UnitTests/Core/Model/StateCommands/AssignedToDraftCommandTests.cs`
- [x] 3.2 Test: `ShouldNotBeValidInWrongStatus` — command with Draft status returns `IsValid() == false`
- [x] 3.3 Test: `ShouldNotBeValidWithWrongEmployee` — command with non-creator user returns `IsValid() == false`
- [x] 3.4 Test: `ShouldBeValid` — command with Assigned status and creator returns `IsValid() == true`
- [x] 3.5 Test: `ShouldTransitionStateProperly` — after `Execute()`, status is Draft, Assignee is null, AssignedDate is null
- [x] 3.6 Update `StateCommandListTests.ShouldReturnAllStateCommandsInCorrectOrder` to expect 7 commands including `AssignedToDraftCommand`

## 4. Integration Tests

- [x] 4.1 Create `src/IntegrationTests/DataAccess/Handlers/StateCommandHandlerForUnassignTests.cs`
- [x] 4.2 Test: `ShouldSaveWorkOrderBackToDraftWithNoAssignee` — persist through `StateCommandHandler`, verify Draft status, null Assignee, null AssignedDate
- [x] 4.3 Test: `ShouldSaveWorkOrderBackToDraftRemotingCommand` — verify serialization round-trip via `SimulateRemoteObject`

## 5. Architecture Documentation

- [x] 5.1 Update `arch/arch-state-workorder.md` state diagram to include `Assigned --> Draft : AssignedToDraftCommand`
