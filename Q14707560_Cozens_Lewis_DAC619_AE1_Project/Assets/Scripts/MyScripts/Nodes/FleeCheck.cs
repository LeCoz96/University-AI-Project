public class FleeCheck : Node
{
    private Sensing _agentSenses;

    public FleeCheck(Sensing agentSenses)
    {
        _agentSenses = agentSenses;
    }

    public override NodeState Decision()
    {
        if (_agentSenses.GetNearestFriendlyInView() == null && _agentSenses.GetEnemiesInView().Count > 1) // If there are no friendlies around this node is successful
            return NodeState.SUCCESS;
        else
            return NodeState.FAILURE;
    }
}
