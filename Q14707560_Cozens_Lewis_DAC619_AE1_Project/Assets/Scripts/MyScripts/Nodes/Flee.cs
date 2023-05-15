using UnityEngine;

public class Flee : Node
{
    private Sensing _agentSenses;
    private AgentActions _agentActions;
    private GameObject _target;

    public Flee(Sensing agentSenses, AgentActions agentActions)
    {
        _agentSenses = agentSenses;
        _agentActions = agentActions;
    }

    public override NodeState Decision()
    {
        _target = _agentSenses.GetNearestEnemyInView(); // Flee from the closest target
        _agentActions.Flee(_target);
        return NodeState.RUNNING;
    }
}
