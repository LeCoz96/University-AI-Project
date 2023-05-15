public class CheckDefenders : Node
{
    AI _agent;

    public CheckDefenders(AI agent)
    {
        _agent = agent;
    }

    public override NodeState Decision()
    {
        if (_agent.gameObject.tag == Tags.BlueTeam) // If tag is blue team then only check blue defenders
        {
            if (BlackBoard._numberOfBlueDefenders < BlackBoard._maxNumberOfDefenders)
                return NodeState.SUCCESS;
            else
                return NodeState.FAILURE;
        }
        else if (_agent.gameObject.tag == Tags.RedTeam) // If tag is red team then only check red defenders
        {
            if (BlackBoard._numberOfRedDefenders < BlackBoard._maxNumberOfDefenders)
                return NodeState.SUCCESS;
            else
                return NodeState.FAILURE;
        }
        else
            return NodeState.FAILURE;
    }
}
