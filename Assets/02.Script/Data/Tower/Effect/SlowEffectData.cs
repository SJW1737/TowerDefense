using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Effect/Slow")]
public class SlowEffectData : EffectData
{
    public float slowRatio;
    public float duration;

    public override ITowerEffect CreateEffect(Tower tower)
    {
        return new SlowEffect(slowRatio, duration);
    }
}