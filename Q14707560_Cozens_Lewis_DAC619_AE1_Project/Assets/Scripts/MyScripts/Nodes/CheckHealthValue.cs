public class CheckHealthValue : Node
{
    private AgentData _agentData;

    public CheckHealthValue(AgentData agentData)
    {
        _agentData = agentData;
    }

    public override NodeState Decision()
    {
        if (_agentData.CurrentHitPoints < (_agentData.MaxHitPoints / 2)) // This node is successful if the AI is under 50% health
            return NodeState.SUCCESS;
        else
            return NodeState.FAILURE;
    }
}
