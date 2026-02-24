using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Effect/Damage")]
public class DamageEffectData : EffectData
{
    public float damage;
    public float damageGrowth; // 강화 1회당 증가량

    public override ITowerEffect CreateEffect(Tower tower)
    {
        return new DamageEffect(damage, damageGrowth);
    }
}