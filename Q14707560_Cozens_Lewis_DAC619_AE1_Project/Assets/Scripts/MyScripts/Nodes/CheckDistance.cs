using UnityEngine;

public class CheckDistance : Node
{
    private AI _agent;
    private GameObject _target;

    public CheckDistance(AI agent, GameObject target)
    {
        _agent = agent;
        _target = target;
    }

    public override NodeState Decision()
    {
        // If the distance betweent he AI and the target is less than the max distance (20.0f) then this node is successful
        float distance = Vector3.Distance(_target.transform.position, _agent.transform.position);
        if (distance < BlackBoard._maxDistance)
            return NodeState.SUCCESS;
        else
            return NodeState.FAILURE;
    }
}
