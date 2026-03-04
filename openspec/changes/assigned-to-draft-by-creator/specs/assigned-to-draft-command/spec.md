## ADDED Requirements

### Requirement: Assigned to Draft state transition exists
The system SHALL include a state command `AssignedToDraftCommand` that transitions a work order from Assigned status to Draft status.

#### Scenario: Command defines correct begin and end statuses
- **WHEN** the `AssignedToDraftCommand` is inspected
- **THEN** its begin status is `Assigned`
- **AND** its end status is `Draft`

#### Scenario: Only the creator can execute
- **GIVEN** a work order in Assigned status
- **WHEN** a user who is NOT the creator attempts to execute `AssignedToDraftCommand`
- **THEN** `IsValid()` returns `false`

#### Scenario: Creator can execute on Assigned work order
- **GIVEN** a work order in Assigned status
- **AND** the current user is the creator of the work order
- **WHEN** `IsValid()` is evaluated
- **THEN** it returns `true`

#### Scenario: Command is invalid for wrong status
- **GIVEN** a work order in Draft status (not Assigned)
- **AND** the current user is the creator
- **WHEN** `IsValid()` is evaluated
- **THEN** it returns `false`

### Requirement: Execution clears assignment data
When the `AssignedToDraftCommand` is executed, the system SHALL clear the Assignee and AssignedDate fields on the work order.

#### Scenario: Assignee and AssignedDate are cleared
- **GIVEN** a work order in Assigned status with an Assignee and AssignedDate set
- **WHEN** `AssignedToDraftCommand.Execute()` is called
- **THEN** the work order status becomes Draft
- **AND** the Assignee is null
- **AND** the AssignedDate is null

### Requirement: Transition verb is "Unassign"
The command SHALL use "Unassign" as its present tense verb and "Unassigned" as its past tense verb, matching the UI button label convention.

#### Scenario: Verb labels
- **WHEN** the command's `TransitionVerbPresentTense` is read
- **THEN** it returns "Unassign"
- **AND** `TransitionVerbPastTense` returns "Unassigned"
