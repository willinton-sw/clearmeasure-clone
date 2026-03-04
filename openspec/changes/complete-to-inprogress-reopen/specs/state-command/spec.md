## ADDED Requirements

### Requirement: CompleteToInProgressCommand state command
A new state command `CompleteToInProgressCommand` SHALL exist in `src/Core/Model/StateCommands/` that transitions a work order from Complete to InProgress. The command SHALL be a record inheriting from `StateCommandBase` following the same pattern as all existing state commands.

#### Scenario: Valid reopen by assignee
- **GIVEN** a work order with `Status = WorkOrderStatus.Complete`
- **AND** the work order has an `Assignee` set
- **AND** the current user is the assignee
- **WHEN** `IsValid()` is called on a `CompleteToInProgressCommand` for this work order and user
- **THEN** it SHALL return `true`

#### Scenario: Invalid reopen — wrong status
- **GIVEN** a work order with `Status = WorkOrderStatus.InProgress` (or any status other than Complete)
- **AND** the current user is the assignee
- **WHEN** `IsValid()` is called
- **THEN** it SHALL return `false`

#### Scenario: Invalid reopen — wrong user
- **GIVEN** a work order with `Status = WorkOrderStatus.Complete`
- **AND** the current user is NOT the assignee
- **WHEN** `IsValid()` is called
- **THEN** it SHALL return `false`

#### Scenario: Execute clears CompletedDate and changes status
- **GIVEN** a work order with `Status = WorkOrderStatus.Complete` and `CompletedDate` set to a non-null value
- **AND** the current user is the assignee
- **WHEN** `Execute(StateCommandContext)` is called
- **THEN** `WorkOrder.CompletedDate` SHALL be `null`
- **AND** `WorkOrder.Status` SHALL be `WorkOrderStatus.InProgress`

### Requirement: Transition verb naming
The `CompleteToInProgressCommand` SHALL use "Reopen" as `TransitionVerbPresentTense` and "Reopened" as `TransitionVerbPastTense`.

#### Scenario: Verb values
- **WHEN** the command properties are examined
- **THEN** `TransitionVerbPresentTense` SHALL be `"Reopen"`
- **AND** `TransitionVerbPastTense` SHALL be `"Reopened"`
- **AND** the `Name` constant SHALL be `"Reopen"`

### Constraints
- The command SHALL be a C# `record` inheriting `StateCommandBase(WorkOrder, CurrentUser)`
- `GetBeginStatus()` SHALL return `WorkOrderStatus.Complete`
- `GetEndStatus()` SHALL return `WorkOrderStatus.InProgress`
- `UserCanExecute()` SHALL return `true` only when `currentUser == WorkOrder.Assignee`
- `Execute()` SHALL set `WorkOrder.CompletedDate = null` before calling `base.Execute(context)`
