using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Effect/Burn")]
public class BurnEffectData : EffectData
{
    public float duration = 3f;
    public float interval = 1f;
    [Range(0f, 1f)]
    public float damageRatio = 0.3f;

    public override ITowerEffect CreateEffect(Tower tower)
    {
        return new BurnEffect(tower, duration, interval, damageRatio);
    }
}
