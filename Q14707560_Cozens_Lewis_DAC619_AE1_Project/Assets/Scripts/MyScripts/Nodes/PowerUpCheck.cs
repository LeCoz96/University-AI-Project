public class PowerUpCheck : Node
{
    private bool _bIsPowerUp;

    public PowerUpCheck(bool bIsPowerUp)
    {
        _bIsPowerUp = bIsPowerUp;
    }

    public override NodeState Decision()
    {
        if (_bIsPowerUp)
            return NodeState.SUCCESS;
        else
            return NodeState.FAILURE;
    }
}
