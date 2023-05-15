public abstract class Node
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    protected NodeState _currentNodeState;
    public NodeState currentNodeState
    {
        get { return _currentNodeState; }
        set { _currentNodeState = currentNodeState; }
    }

    public abstract NodeState Decision();
}
