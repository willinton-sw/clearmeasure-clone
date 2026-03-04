## ADDED Requirements

### Requirement: Unit tests for CompleteToInProgressCommand
Unit tests SHALL be added in `src/UnitTests/Core/Model/StateCommands/CompleteToInProgressCommandTests.cs`. The test class SHALL extend `StateCommandBaseTests` and follow the same pattern as `InProgressToCompleteCommandTests.cs`.

#### Scenario: ShouldNotBeValidInWrongStatus
- **GIVEN** a work order with `Status = WorkOrderStatus.InProgress` and the current user as assignee
- **WHEN** `IsValid()` is called on `CompleteToInProgressCommand`
- **THEN** it SHALL return `false`

#### Scenario: ShouldNotBeValidWithWrongEmployee
- **GIVEN** a work order with `Status = WorkOrderStatus.Complete` and a different employee as the command user
- **WHEN** `IsValid()` is called
- **THEN** it SHALL return `false`

#### Scenario: ShouldBeValid
- **GIVEN** a work order with `Status = WorkOrderStatus.Complete` and the current user as assignee
- **WHEN** `IsValid()` is called
- **THEN** it SHALL return `true`

#### Scenario: ShouldTransitionStateProperly
- **GIVEN** a work order with `Status = WorkOrderStatus.Complete`, `CompletedDate` set, and the current user as assignee
- **WHEN** `Execute(StateCommandContext)` is called
- **THEN** `order.Status` SHALL be `WorkOrderStatus.InProgress`
- **AND** `order.CompletedDate` SHALL be `null`

### Requirement: StateCommandList test updated
The `ShouldReturnAllStateCommandsInCorrectOrder` test SHALL assert 7 commands and verify `CompleteToInProgressCommand` at index 5.

### Requirement: Integration test for CompleteToInProgressCommand handler
An integration test SHALL be added in `src/IntegrationTests/DataAccess/Handlers/StateCommandHandlerForReopenTests.cs` following the same pattern as `StateCommandHandlerForCompleteTests.cs`.

#### Scenario: ShouldReopenWorkOrder
- **GIVEN** a work order persisted in the database with `Status = Complete` and `CompletedDate` set
- **AND** the current user is both creator and assignee
- **WHEN** `StateCommandHandler.Handle(CompleteToInProgressCommand)` is executed
- **THEN** the work order in the database SHALL have `Status = InProgress`
- **AND** `CompletedDate` SHALL be `null`
- **AND** `Creator` and `Assignee` SHALL be preserved

### Constraints
- Unit test file: `src/UnitTests/Core/Model/StateCommands/CompleteToInProgressCommandTests.cs`
- Integration test file: `src/IntegrationTests/DataAccess/Handlers/StateCommandHandlerForReopenTests.cs`
- Tests SHALL use NUnit `[TestFixture]` and `[Test]` attributes
- Unit tests SHALL use `Assert.That` assertions (matching existing test pattern)
- Integration tests SHALL use Shouldly assertions (matching existing pattern)
- Integration tests SHALL use `Faker<T>()` for test data generation
- Integration tests SHALL use `RemotableRequestTests.SimulateRemoteObject()` to test serialization round-trip
