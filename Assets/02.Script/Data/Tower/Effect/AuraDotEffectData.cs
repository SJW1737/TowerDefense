using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Effect/Aura DOT")]
public class AuraDotEffectData : EffectData
{
    public float radius;
    public float damageRatio;
    public float duration;
    public float tickInterval;
    public LayerMask monsterLayer;

    public override ITowerEffect CreateEffect(Tower tower)
    {
        DamageEffect damageEffect = tower.GetEffect<DamageEffect>();

        return new AuraDotEffect(damageEffect, radius, damageRatio, duration, tickInterval, monsterLayer);
    }
}
