using UnityEngine;

public class DefendFlagRunner : Node
{
    private AgentData _agentData;
    private AgentActions _agentActions;

    public DefendFlagRunner(AgentData agentData, AgentActions agentActions)
    {
        _agentData = agentData;
        _agentActions = agentActions;
    }

    public override NodeState Decision()
    {
        if (FlagIsAtEnemyBase())
            return NodeState.FAILURE;
        else // If the target flag isn't in the target base, get the team mate who has the flag and follow them
        {
            var FlagRunner = GameObject.Find(_agentData.EnemyFlagName).transform.parent;

            if (FlagRunner)
            {
                _agentActions.MoveTo(FlagRunner.gameObject);
                return NodeState.RUNNING;
            }
            else
                return NodeState.FAILURE;
        }
    }

    private bool FlagIsAtEnemyBase() // If target flag is not in the target base return true
    {
        if (_agentData.EnemyBase.GetComponent<BoxCollider>().bounds.Intersects(GameObject.Find(_agentData.EnemyFlagName).GetComponent<BoxCollider>().bounds))
            return true;
        else
            return false;
    }
}
