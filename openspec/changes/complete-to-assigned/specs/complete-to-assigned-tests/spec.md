## ADDED Requirements

### Requirement: Unit tests for CompleteToAssignedCommand
The system SHALL include unit tests in `src/UnitTests/Core/Model/StateCommands/CompleteToAssignedCommandTests.cs` validating the command behavior.

#### Scenario: ShouldNotBeValidInWrongStatus
- **GIVEN** a work order with status **Draft** and the current user is the Creator
- **WHEN** `CompleteToAssignedCommand.IsValid()` is evaluated
- **THEN** the result SHALL be `false`

#### Scenario: ShouldNotBeValidWithWrongEmployee
- **GIVEN** a work order with status **Complete** and the current user is NOT the Creator
- **WHEN** `CompleteToAssignedCommand.IsValid()` is evaluated
- **THEN** the result SHALL be `false`

#### Scenario: ShouldBeValid
- **GIVEN** a work order with status **Complete** and the current user is the Creator
- **WHEN** `CompleteToAssignedCommand.IsValid()` is evaluated
- **THEN** the result SHALL be `true`

#### Scenario: ShouldTransitionStateProperly
- **GIVEN** a work order with status **Complete** and the current user is the Creator
- **WHEN** `CompleteToAssignedCommand.Execute()` is called
- **THEN** the work order status SHALL be **Assigned**

#### Scenario: ShouldClearCompletedDate
- **GIVEN** a work order with status **Complete** and a non-null `CompletedDate`
- **WHEN** `CompleteToAssignedCommand.Execute()` is called
- **THEN** `CompletedDate` SHALL be `null`

### Requirement: Integration test for StateCommandHandler with CompleteToAssignedCommand
The system SHALL include an integration test in `src/IntegrationTests/DataAccess/Handlers/StateCommandHandlerForReassignTests.cs` validating end-to-end persistence.

#### Scenario: ShouldReassignWorkOrder
- **GIVEN** a work order persisted in the database with status **Complete** and a `CompletedDate`
- **AND** the work order has a distinct Creator and Assignee
- **WHEN** the `CompleteToAssignedCommand` is handled by the `StateCommandHandler`
- **THEN** the persisted work order status SHALL be **Assigned**
- **AND** the persisted `CompletedDate` SHALL be `null`
- **AND** the Creator and Assignee SHALL be unchanged

### Requirement: StateCommandList count test update
The existing test `StateCommandListTests.ShouldReturnAllStateCommandsInCorrectOrder` SHALL be updated to expect 7 commands (previously 6) and assert that index 6 is an instance of `CompleteToAssignedCommand`.

### Constraints

- Unit tests SHALL use NUnit 4.x with `Assert.That()` assertions
- Integration tests SHALL use Shouldly assertions (e.g., `ShouldBe`, `ShouldBeNull`)
- Tests SHALL follow the AAA pattern without section comments
- Test class SHALL inherit from `StateCommandBaseTests`
