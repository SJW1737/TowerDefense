using UnityEngine;

public class FireDotEffect : ITowerEffect
{
    private readonly DamageEffect damageEffect;
    private readonly float damageRatio = 0.3f;
    private readonly float duration = 3f;
    private readonly float tickInterval;

    public FireDotEffect(DamageEffect damageEffect, float tickInterval)
    {
        this.damageEffect = damageEffect;
        this.tickInterval = tickInterval;
    }

    public void Apply(Monster target)
    {
        float damagePerTick = damageEffect.GetFinalDamage() * damageRatio;
        target.ApplyDot(damagePerTick, duration, tickInterval);
    }
}
