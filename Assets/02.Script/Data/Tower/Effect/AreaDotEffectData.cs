using UnityEngine;

public enum AreaDotCenterType
{
    Tower,
    Target
}

[CreateAssetMenu(menuName = "Tower/Effect/AreaDot")]
public class AreaDotEffectData : EffectData
{
    public AreaDotCenterType centerType;
    public float radius;
    public float duration;
    [Range(0f, 1f)]
    public float damageRatio;

    public override ITowerEffect CreateEffect(Tower tower)
    {
        return new AreaDotEffect(tower, centerType, radius, duration, damageRatio);
    }
}
