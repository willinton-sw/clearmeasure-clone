## ADDED Requirements

### Requirement: Complete to Assigned state command
The system SHALL provide a state command `CompleteToAssignedCommand` that transitions a work order from **Complete** status to **Assigned** status.

#### Scenario: Creator transitions completed work order to Assigned
- **GIVEN** a work order exists with status **Complete**
- **AND** the current user is the **Creator** of the work order
- **WHEN** the `CompleteToAssignedCommand` is executed
- **THEN** the work order status SHALL change to **Assigned**
- **AND** the `CompletedDate` SHALL be set to `null`

#### Scenario: Non-creator cannot execute the command
- **GIVEN** a work order exists with status **Complete**
- **AND** the current user is NOT the **Creator** of the work order
- **WHEN** the `CompleteToAssignedCommand.IsValid()` is evaluated
- **THEN** the result SHALL be `false`

#### Scenario: Command is invalid for non-Complete statuses
- **GIVEN** a work order exists with a status other than **Complete** (e.g., Draft, Assigned, InProgress)
- **AND** the current user is the **Creator** of the work order
- **WHEN** the `CompleteToAssignedCommand.IsValid()` is evaluated
- **THEN** the result SHALL be `false`

### Requirement: Command registration in StateCommandList
The `StateCommandList.GetAllStateCommands()` SHALL include `CompleteToAssignedCommand` in the list of all state commands.

#### Scenario: CompleteToAssignedCommand appears in all commands
- **WHEN** `StateCommandList.GetAllStateCommands()` is called with any work order and employee
- **THEN** the returned array SHALL contain an instance of `CompleteToAssignedCommand`

#### Scenario: Command appears as valid for eligible work orders
- **GIVEN** a work order with status **Complete**
- **AND** the current user is the **Creator**
- **WHEN** `StateCommandList.GetValidStateCommands()` is called
- **THEN** the returned array SHALL include the `CompleteToAssignedCommand`
- **AND** its `TransitionVerbPresentTense` SHALL be `"Reassign"`

### Constraints

- The command SHALL follow the existing `StateCommandBase` pattern with `GetBeginStatus()`, `GetEndStatus()`, `UserCanExecute()`, and `Execute()` overrides
- The command SHALL NOT publish any `IStateTransitionEvent` (no NServiceBus events)
- The command SHALL preserve the existing `Assignee` — only the status and `CompletedDate` change
- The command SHALL be placed in the `ClearMeasure.Bootcamp.Core.Model.StateCommands` namespace
