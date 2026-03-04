using ClearMeasure.Bootcamp.Core.Services;

namespace ClearMeasure.Bootcamp.Core.Model.StateCommands;

/// <summary>
/// Transitions a work order from Assigned back to Draft.
/// Only the Creator of the work order can execute this command.
/// Clears the Assignee and AssignedDate.
/// </summary>
public record AssignedToDraftCommand(WorkOrder WorkOrder, Employee CurrentUser)
    : StateCommandBase(WorkOrder, CurrentUser)
{
    public const string Name = "Unassign";

    public override string TransitionVerbPresentTense => Name;

    public override string TransitionVerbPastTense => "Unassigned";

    public override WorkOrderStatus GetBeginStatus()
    {
        return WorkOrderStatus.Assigned;
    }

    public override WorkOrderStatus GetEndStatus()
    {
        return WorkOrderStatus.Draft;
    }

    protected override bool UserCanExecute(Employee currentUser)
    {
        return currentUser == WorkOrder.Creator;
    }

    public override void Execute(StateCommandContext context)
    {
        WorkOrder.AssignedDate = null;
        WorkOrder.Assignee = null;
        base.Execute(context);
    }
}
