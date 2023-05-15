using System.Collections.Generic;
using UnityEngine;

public class WithinDetectionRange : Node
{
    private Sensing _agentSenses;

    public WithinDetectionRange(Sensing agentSenses)
    {
        _agentSenses = agentSenses;
    }

    public override NodeState Decision()
    {
        if (EnemiesInDetectionRange())
            return NodeState.SUCCESS;
        else
            return NodeState.FAILURE;
    }

    private bool EnemiesInDetectionRange()
    {
        List<GameObject> enemies = _agentSenses.GetEnemiesInView();
        return enemies.Count > 0 ? true : false; // If there is an target in view then return true
    }
}
