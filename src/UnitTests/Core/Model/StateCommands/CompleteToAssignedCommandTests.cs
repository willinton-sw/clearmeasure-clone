using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Model.StateCommands;
using ClearMeasure.Bootcamp.Core.Services;

namespace ClearMeasure.Bootcamp.UnitTests.Core.Model.StateCommands;

[TestFixture]
public class CompleteToAssignedCommandTests : StateCommandBaseTests
{
    [Test]
    public void ShouldNotBeValidInWrongStatus()
    {
        var order = new WorkOrder();
        order.Status = WorkOrderStatus.Draft;
        var creator = new Employee();
        order.Creator = creator;

        var command = new CompleteToAssignedCommand(order, creator);
        Assert.That(command.IsValid(), Is.False);
    }

    [Test]
    public void ShouldNotBeValidWithWrongEmployee()
    {
        var order = new WorkOrder();
        order.Status = WorkOrderStatus.Complete;
        var creator = new Employee();
        order.Creator = creator;

        var command = new CompleteToAssignedCommand(order, new Employee());
        Assert.That(command.IsValid(), Is.False);
    }

    [Test]
    public void ShouldBeValid()
    {
        var order = new WorkOrder();
        order.Status = WorkOrderStatus.Complete;
        var creator = new Employee();
        order.Creator = creator;

        var command = new CompleteToAssignedCommand(order, creator);
        Assert.That(command.IsValid(), Is.True);
    }

    [Test]
    public void ShouldTransitionStateProperly()
    {
        var order = new WorkOrder();
        order.Number = "123";
        order.Status = WorkOrderStatus.Complete;
        order.CompletedDate = DateTime.Now;
        var creator = new Employee();
        order.Creator = creator;

        var command = new CompleteToAssignedCommand(order, creator);
        command.Execute(new StateCommandContext());

        Assert.That(order.Status, Is.EqualTo(WorkOrderStatus.Assigned));
    }

    [Test]
    public void ShouldClearCompletedDate()
    {
        var order = new WorkOrder();
        order.Number = "123";
        order.Status = WorkOrderStatus.Complete;
        order.CompletedDate = DateTime.Now;
        var creator = new Employee();
        order.Creator = creator;

        var command = new CompleteToAssignedCommand(order, creator);
        command.Execute(new StateCommandContext());

        Assert.That(order.CompletedDate, Is.Null);
    }

    protected override StateCommandBase GetStateCommand(WorkOrder order, Employee employee)
    {
        return new CompleteToAssignedCommand(order, employee);
    }
}
