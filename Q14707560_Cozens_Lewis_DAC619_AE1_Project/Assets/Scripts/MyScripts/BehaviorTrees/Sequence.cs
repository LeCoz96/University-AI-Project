using System.Collections.Generic;

public class Sequence : Node
{
    protected List<Node> nodes = new List<Node>();

    public Sequence(List<Node> node)
    {
        nodes = node;
    }

    public override NodeState Decision()
    {
        foreach (Node node in nodes)
        {
            switch (node.Decision())
            {
                case NodeState.RUNNING:
                    return _currentNodeState = NodeState.RUNNING;
                case NodeState.SUCCESS:
                    break;
                case NodeState.FAILURE:
                    return _currentNodeState = NodeState.FAILURE;
            }
        }
        return _currentNodeState = NodeState.SUCCESS;
    }
}
