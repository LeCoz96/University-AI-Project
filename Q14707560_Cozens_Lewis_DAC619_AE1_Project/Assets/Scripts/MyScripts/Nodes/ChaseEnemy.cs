using UnityEngine;

public class ChaseEnemy : Node
{
    private Sensing _agentSenses;
    private AgentActions _agentActions;

    public ChaseEnemy( Sensing agentSenses, AgentActions agentActions)
    {
        _agentSenses = agentSenses;
        _agentActions = agentActions;
    }

    public override NodeState Decision()
    {
         // Checks the closest target to the player and checks if they're within attack range
        GameObject _target = _agentSenses.GetNearestEnemyInView();

        if (!_agentSenses.IsInAttackRange(_target)) // If they are out of attack range then then move to the target
        {
            _agentActions.MoveTo(_target.transform.position);
            return NodeState.RUNNING;
        }
        else // If the target is in attack range this node is successful
            return NodeState.SUCCESS;
    }
}
