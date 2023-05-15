public class Inverter : Node
{
    protected Node iNodes;

    public Inverter(Node node)
    {
        iNodes = node;
    }

    public override NodeState Decision()
    {
        switch (iNodes.Decision())
        {
            case NodeState.RUNNING:
                _currentNodeState = NodeState.RUNNING;
                break;
            case NodeState.SUCCESS:
                _currentNodeState = NodeState.FAILURE;
                break;
            case NodeState.FAILURE:
                _currentNodeState = NodeState.SUCCESS;
                break;
            default:
                break;
        }
        return _currentNodeState;
    }
}
