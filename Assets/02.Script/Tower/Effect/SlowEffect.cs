public class SlowEffect : ITowerEffect
{
    private float slowRatio;
    private float duration;

    public SlowEffect(float slowRatio, float duration)
    {
        this.slowRatio = slowRatio;
        this.duration = duration;
    }

    public void Apply(Monster target)
    {
        target.ApplySlow(slowRatio, duration);
    }
}
