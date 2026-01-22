using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Effect/DOT")]
public class DOTEffectData : EffectData
{
    public float duration;
    public float interval;
    [Range(0f, 1f)]
    public float damageRatio;

    public override ITowerEffect CreateEffect(Tower tower)
    {
        return new DOTEffect(tower, duration, interval, damageRatio);
    }
}
