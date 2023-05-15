using System.Collections.Generic;

public class Selector : Node
{
    protected List<Node> nodes = new List<Node>();

    public Selector(List<Node> node)
    {
        nodes = node;
    }

    public override NodeState Decision()
    {
        foreach (Node node in nodes)
        {
            switch (node.Decision())
            {
                case NodeState.SUCCESS:
                    _currentNodeState = NodeState.SUCCESS;
                    return _currentNodeState;
                case NodeState.FAILURE:
                    continue;
                case NodeState.RUNNING:
                    _currentNodeState = NodeState.RUNNING;
                    return _currentNodeState;
                default:
                    break;
            }
        }
        _currentNodeState = Node.NodeState.FAILURE;
        return _currentNodeState;
    }
}
