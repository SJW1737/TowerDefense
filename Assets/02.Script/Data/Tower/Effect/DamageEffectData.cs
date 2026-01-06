using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Effect/Damage")]
public class DamageEffectData : EffectData
{
    public int damage;

    public override ITowerEffect CreateEffect()
    {
        return new DamageEffect(damage);
    }
}