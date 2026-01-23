public class IceFrozenEffect : ITowerEffect
{
    private float duration;

    public IceFrozenEffect(float duration)
    {
        this.duration = duration;
    }

    public void Apply(Monster target)
    {
        target.ApplyFrozen(duration);
    }
}
