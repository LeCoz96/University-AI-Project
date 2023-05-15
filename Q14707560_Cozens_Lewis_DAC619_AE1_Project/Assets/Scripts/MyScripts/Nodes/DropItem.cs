using UnityEngine;

public class DropItem : Node
{
    private AgentActions _agentActions;
    private GameObject _target;

    public DropItem(AgentActions agentActions, GameObject target)
    {
        _agentActions = agentActions;
        _target = target;
    }

    public override NodeState Decision()
    {
        _agentActions.DropItem(_target);
        return NodeState.SUCCESS;
    }
}
