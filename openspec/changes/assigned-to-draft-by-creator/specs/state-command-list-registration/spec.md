## MODIFIED Requirements

### Requirement: StateCommandList includes AssignedToDraftCommand
The `StateCommandList.GetAllStateCommands()` method SHALL include `AssignedToDraftCommand` in its returned array, so that the command is available for validation and execution.

#### Scenario: All commands are returned in correct order
- **WHEN** `GetAllStateCommands()` is called
- **THEN** 7 commands are returned
- **AND** the commands include `AssignedToDraftCommand` after `AssignedToCancelledCommand`

#### Scenario: Valid commands filtered for creator on Assigned work order
- **GIVEN** a work order in Assigned status
- **AND** the current user is the creator
- **WHEN** `GetValidStateCommands()` is called
- **THEN** the result includes `AssignedToDraftCommand`
- **AND** the result includes `AssignedToCancelledCommand`
