using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Effect/AreaDot")]
public class AreaDotEffectData : EffectData
{
    public float duration;
    [Range(0f, 1f)]
    public float damageRatio;

    public override ITowerEffect CreateEffect(Tower tower)
    {
        return new AreaDotEffect(tower, duration, damageRatio);
    }
}
