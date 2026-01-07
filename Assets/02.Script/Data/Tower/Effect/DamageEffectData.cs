using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Effect/Damage")]
public class DamageEffectData : EffectData
{
    public int damage;
    public int damageGrowth; // 강화 1회당 증가량

    public override ITowerEffect CreateEffect()
    {
        return new DamageEffect(damage);
    }
}