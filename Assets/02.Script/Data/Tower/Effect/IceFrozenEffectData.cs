using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Effect/Frozen")]
public class IceFrozenEffectData : EffectData
{
    public float duration;

    public override ITowerEffect CreateEffect(Tower tower)
    {
        return new IceFrozenEffect(duration);
    }
}
