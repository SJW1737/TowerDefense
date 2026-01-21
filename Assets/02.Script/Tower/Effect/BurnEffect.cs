using UnityEngine;

public class BurnEffect : ITowerEffect
{
    private readonly Tower tower;
    private readonly float duration;
    private readonly float interval;
    private readonly float damageRatio;

    public BurnEffect(Tower tower, float duration, float interval, float damageRatio)
    {
        this.tower = tower;
        this.duration = duration;
        this.interval = interval;
        this.damageRatio = damageRatio;
    }

    public void Apply(Monster target)
    {
        int baseDamage = tower.GetEffect<DamageEffect>().GetFinalDamage();
        int burnDamage = Mathf.Max(1, Mathf.RoundToInt(baseDamage * damageRatio));

        target.ApplyBurn(burnDamage, duration, interval);
    }
}
