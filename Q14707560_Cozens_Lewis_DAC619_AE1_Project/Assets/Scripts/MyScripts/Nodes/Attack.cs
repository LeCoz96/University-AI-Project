using UnityEngine;

public class Attack : Node
{
    private AgentActions _agentActions;
    private Sensing _agentSenses;

    public Attack(AgentActions agentActions, Sensing agentSenses)
    {
        _agentActions = agentActions;
        _agentSenses = agentSenses;
    }

    public override NodeState Decision()
    {
        GameObject enemy = _agentSenses.GetNearestEnemyInView(); // Get the closest target to the player
        _agentActions.MoveTo(enemy.transform.position); // Keep close to the target
        _agentActions.AttackEnemy(enemy); // Attack the target
        return NodeState.SUCCESS;
    }
}
