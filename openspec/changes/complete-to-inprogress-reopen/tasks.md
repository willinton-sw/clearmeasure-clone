## 1. State Command Implementation

- [x] 1.1 Create `src/Core/Model/StateCommands/CompleteToInProgressCommand.cs` — record inheriting `StateCommandBase`, begin=Complete, end=InProgress, authorized by assignee, clears `CompletedDate` in `Execute()`
- [x] 1.2 Register `CompleteToInProgressCommand` in `StateCommandList.GetAllStateCommands()` after `InProgressToCompleteCommand`

## 2. Unit Tests

- [x] 2.1 Create `src/UnitTests/Core/Model/StateCommands/CompleteToInProgressCommandTests.cs` with tests: wrong status, wrong user, valid, state transition (status change + CompletedDate cleared)
- [x] 2.2 Update `StateCommandListTests.ShouldReturnAllStateCommandsInCorrectOrder` — count 6→7, verify `CompleteToInProgressCommand` at index 5

## 3. Integration Tests

- [x] 3.1 Create `src/IntegrationTests/DataAccess/Handlers/StateCommandHandlerForReopenTests.cs` — full handler round-trip: persist completed work order, execute command, verify DB state (status=InProgress, CompletedDate=null)

## 4. Acceptance Test Fix

- [x] 4.1 Update `ShouldShowSpeakButtonsOnReadOnlyWorkOrder` in `WorkOrderSpeechTests.cs` — remove `ReadOnlyMessage` assertion since completed work orders are no longer read-only for the assignee; verify Reopen button is visible instead

## 5. Documentation

- [x] 5.1 Update `arch/arch-state-workorder.md` state diagram — add `Complete --> InProgress : CompleteToInProgressCommand (Reopen)`
- [x] 5.2 Create `arch/WorflowForCompleteToInProgressCommand.md` sequence diagram following existing pattern
