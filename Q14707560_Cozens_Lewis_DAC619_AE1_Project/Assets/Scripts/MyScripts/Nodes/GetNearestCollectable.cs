using UnityEngine;

public class GetNearestCollectable : Node
{
    private AI _agent;
    private Sensing _agentSenses;
    private AgentActions _agentActions;
    private string _target;
    private GameObject _closestTarget = null;
    private float _distance = 100.0f;

    public GetNearestCollectable(AI agent, Sensing agentSenses, AgentActions agentActions, string target)
    {
        _agent = agent;
        _agentSenses = agentSenses;
        _agentActions = agentActions;
        _target = target;
    }

    public override NodeState Decision()
    {
        if (NearestCollectable() != null)
        {
            float distance = Vector3.Distance(_closestTarget.transform.position, _agent.transform.position);
            if (distance > BlackBoard._minDistance)
            {
                _agentActions.MoveTo(_closestTarget.transform.position);
                return NodeState.RUNNING;
            }
            else
                return NodeState.SUCCESS;
        }
        else
            return NodeState.FAILURE;
    }

    private GameObject NearestCollectable()
    {
        foreach (GameObject item in _agentSenses.GetCollectablesInView()) // Search all local collectables
        {
            if (item.name == _target) // If an item in the list matches the target item that is the target object
            {
                float _itemDistance = (item.transform.position - _agent.transform.position).magnitude;

                if (_itemDistance < _distance)
                {
                    _distance = _itemDistance; // record item distance in case there's 2 choices and one is closer than the other
                    _closestTarget = item;
                }
            }
        }
        return _closestTarget;
    }
}
