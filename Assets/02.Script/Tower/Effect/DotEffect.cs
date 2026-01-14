public class DotEffect : ITowerEffect
{
    private readonly DamageEffect damageEffect;
    private readonly float damageRatio;
    private readonly float duration;
    private readonly float tickInterval;

    public DotEffect(DamageEffect damageEffect, float damageRatio, float duration, float tickInterval)
    {
        this.damageEffect = damageEffect;
        this.damageRatio = damageRatio;
        this.duration = duration;
        this.tickInterval = tickInterval;
    }

    public void Apply(Monster target)
    {
        float damagePerTick = damageEffect.GetFinalDamage() * damageRatio;

        target.ApplyDot(damagePerTick, duration, tickInterval);
    }
}
