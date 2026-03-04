## MODIFIED Requirements

### Requirement: StateCommandList includes CompleteToInProgressCommand
The `StateCommandList.GetAllStateCommands()` method SHALL include `CompleteToInProgressCommand` in its returned array, positioned after `InProgressToCompleteCommand` and before `AssignedToCancelledCommand`.

#### Scenario: Command list includes reopen command
- **WHEN** `GetAllStateCommands()` is called with any work order and employee
- **THEN** the returned array SHALL contain 7 commands
- **AND** the command at index 5 SHALL be an instance of `CompleteToInProgressCommand`

### Requirement: UI surfaces Reopen button for assignee on completed work order
The existing `WorkOrderManage.razor` component dynamically renders buttons for all valid state commands via `StateCommandList.GetValidStateCommands()`. No UI code changes are required. The Reopen button SHALL automatically appear when the logged-in user is the assignee of a completed work order.

#### Scenario: Assignee views completed work order
- **GIVEN** a work order with `Status = Complete`
- **AND** the logged-in user is the work order's assignee
- **WHEN** the work order manage page renders
- **THEN** the page SHALL display a "Reopen" button with `data-testid="CommandButtonReopen"`
- **AND** the page SHALL NOT display the read-only message

#### Scenario: Non-assignee views completed work order
- **GIVEN** a work order with `Status = Complete`
- **AND** the logged-in user is NOT the work order's assignee
- **WHEN** the work order manage page renders
- **THEN** the page SHALL display the read-only message
- **AND** no action buttons SHALL be displayed

### Requirement: Acceptance test updated for new reopen behavior
The `ShouldShowSpeakButtonsOnReadOnlyWorkOrder` test in `WorkOrderSpeechTests.cs` SHALL be updated to account for the fact that a completed work order is no longer read-only when the logged-in user is the assignee.

#### Scenario: Updated test verifies speak buttons with reopen capability
- **GIVEN** the test creates, assigns, begins, and completes a work order as the current user
- **WHEN** the test navigates to the completed work order
- **THEN** the Reopen button SHALL be visible (confirming assignee has actions available)
- **AND** the SpeakTitle button SHALL be visible
- **AND** the SpeakDescription button SHALL be visible

### Constraints
- No changes to `WorkOrderManage.razor` or `WorkOrderManage.razor.cs` SHALL be required — the UI auto-discovers valid commands
- The Reopen button SHALL use the same CSS class (`btn btn-primary`) as all other command buttons
- The button `data-testid` SHALL follow the pattern `CommandButton` + verb = `CommandButtonReopen`
