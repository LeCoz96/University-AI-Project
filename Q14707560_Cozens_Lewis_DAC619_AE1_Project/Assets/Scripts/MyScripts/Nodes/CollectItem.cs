using UnityEngine;

public class CollectItem : Node
{
    private AgentActions _agentActions;
    private Sensing _agentSenses;
    private GameObject _target;

    public CollectItem(AgentActions agentActions, Sensing agentSenses, GameObject target)
    {
        _agentActions = agentActions;
        _agentSenses = agentSenses;
        _target = target;
    }

    public override NodeState Decision()
    {
        if (_target == null) // If there is no direct target then target the closest collectable
            _target = _agentSenses.GetNearestCollectableInView();

        _agentActions.CollectItem(_target); // collect the target object
        return NodeState.SUCCESS;
    }
}
