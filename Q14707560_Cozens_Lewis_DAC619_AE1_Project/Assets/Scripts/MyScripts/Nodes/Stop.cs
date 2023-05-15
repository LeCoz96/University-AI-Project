public class Stop : Node
{
    private AgentActions _agentActions;

    public Stop(AgentActions agentActions)
    {
        _agentActions = agentActions;
    }

    public override NodeState Decision()
    {
        _agentActions.Stop();
        return NodeState.SUCCESS;
    }
}
