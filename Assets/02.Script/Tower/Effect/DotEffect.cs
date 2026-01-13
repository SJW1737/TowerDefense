using UnityEngine;

public class DotEffect : ITowerEffect
{
    private float damagePerTick;
    private float duration;
    private float interval;

    public DotEffect(float damagePerTick, float duration, float interval)
    {
        this.damagePerTick = damagePerTick;
        this.duration = duration;
        this.interval = interval;
    }

    public void Apply(Monster target)
    {
        target.ApplyDot(damagePerTick, duration, interval);
    }
}
