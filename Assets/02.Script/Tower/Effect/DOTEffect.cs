using UnityEngine;

public class DOTEffect : ITowerEffect
{
    private readonly Tower tower;
    private readonly float duration;
    private readonly float interval;
    private readonly float damageRatio;

    public DOTEffect(Tower tower, float duration, float interval, float damageRatio)
    {
        this.tower = tower;
        this.duration = duration;
        this.interval = interval;
        this.damageRatio = damageRatio;
    }

    public void Apply(Monster target)
    {
        int baseDamage = tower.GetEffect<DamageEffect>().GetFinalDamage();
        int dotDamage = Mathf.Max(1, Mathf.RoundToInt(baseDamage * damageRatio));

        if (target.TryGetComponent(out MonsterBurn burn))
        {
            burn.ApplyBurn(dotDamage, duration, interval);
        }
        
        if (target.TryGetComponent(out MonsterPoison poison))
        {
            poison.ApplyPoison(dotDamage, duration, interval);
        }
    }
}
