using ClearMeasure.Bootcamp.Core.Services;

namespace ClearMeasure.Bootcamp.Core.Model.StateCommands;

/// <summary>
/// Transitions a work order from Complete back to Assigned.
/// Only the Creator of the work order can execute this command.
/// </summary>
public record CompleteToAssignedCommand(WorkOrder WorkOrder, Employee CurrentUser)
    : StateCommandBase(WorkOrder, CurrentUser)
{
    public const string Name = "Reassign";

    public override string TransitionVerbPresentTense => Name;

    public override string TransitionVerbPastTense => "Reassigned";

    public override WorkOrderStatus GetBeginStatus()
    {
        return WorkOrderStatus.Complete;
    }

    public override WorkOrderStatus GetEndStatus()
    {
        return WorkOrderStatus.Assigned;
    }

    protected override bool UserCanExecute(Employee currentUser)
    {
        return currentUser == WorkOrder.Creator;
    }

    public override void Execute(StateCommandContext context)
    {
        WorkOrder.CompletedDate = null;
        base.Execute(context);
    }
}
