public class CheckInventory : Node
{
    private InventoryController _agentInventory;
    private string _target;

    public CheckInventory(InventoryController agentInventory, string target)
    {
        _agentInventory = agentInventory;
        _target = target;
    }

    public override NodeState Decision()
    {
        if (_agentInventory.HasItem(_target)) // If the AI has the target object from the constructor then this node is successful
            return NodeState.SUCCESS;
        else
            return NodeState.FAILURE;
    }
}
