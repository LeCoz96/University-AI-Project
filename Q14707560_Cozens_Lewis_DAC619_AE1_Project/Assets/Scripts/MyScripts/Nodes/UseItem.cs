using UnityEngine;

public class UseItem : Node
{
    private AgentActions _agentActions;
    private GameObject _item;

    public UseItem(AgentActions agentActions, GameObject item)
    {
        _agentActions = agentActions;
        _item = item;
    }

    public override NodeState Decision()
    {
        _agentActions.UseItem(_item);
        return NodeState.SUCCESS;
    }
}
