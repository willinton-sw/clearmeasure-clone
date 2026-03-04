## MODIFIED Requirements

### Requirement: UI shows Unassign button for creator on Assigned work orders
The work order management page SHALL display an "Unassign" action button when the current user is the creator and the work order is in Assigned status. This is handled automatically by the existing dynamic command button rendering in `WorkOrderManage.razor`.

#### Scenario: Creator sees Unassign button
- **GIVEN** a work order in Assigned status
- **AND** the current user is the creator
- **WHEN** the work order management page is loaded
- **THEN** an "Unassign" button is displayed in the action section

#### Scenario: Assignee does not see Unassign button
- **GIVEN** a work order in Assigned status
- **AND** the current user is the assignee (not the creator)
- **WHEN** the work order management page is loaded
- **THEN** no "Unassign" button is displayed

#### Scenario: Unassign returns work order to editable Draft state
- **GIVEN** a work order in Assigned status with the assignee dropdown disabled
- **WHEN** the creator clicks "Unassign"
- **THEN** the work order transitions to Draft status
- **AND** the assignee dropdown becomes editable again (since `CanReassign()` returns true for Draft)
