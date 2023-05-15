public class PowerUpTrigger : Node
{
    private BTFlagRunner FRScript;

    public PowerUpTrigger(BTFlagRunner fRScript)
    {
        FRScript = fRScript;
    }

    public override NodeState Decision()
    {
        FRScript.PowerUpComplete(); // Once the power up has been collected the AI will no longer be a power up
        return NodeState.SUCCESS;
    }
}
