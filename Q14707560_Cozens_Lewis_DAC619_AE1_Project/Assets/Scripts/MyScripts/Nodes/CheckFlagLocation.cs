using UnityEngine;

public class CheckFlagLocation : Node
{
    private GameObject _targetFlag;
    private GameObject _targetBase;

    public CheckFlagLocation(GameObject targetFlag, GameObject targetBase)
    {
        _targetFlag = targetFlag;
        _targetBase = targetBase;
    }

    public override NodeState Decision()
    {
        if (!FlagIsAtBase()) // If the flag is outside of the base this node is successful
            return NodeState.SUCCESS;
        else
            return NodeState.FAILURE;
    }

    private bool FlagIsAtBase() // Check to see if the target flag is in inside the range of the target base
    {
        if (_targetBase.GetComponent<BoxCollider>().bounds.Intersects(_targetFlag.GetComponent<BoxCollider>().bounds))
            return true;
        else
            return false;
    }
}
