using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Effect/Burn")]
public class BurnEffectData : EffectData
{
    public float duration;
    public float interval;
    [Range(0f, 1f)]
    public float damageRatio;

    public override ITowerEffect CreateEffect(Tower tower)
    {
        return new BurnEffect(tower, duration, interval, damageRatio);
    }
}
