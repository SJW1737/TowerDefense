using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Attack/Beam")]
public class BeamAttackData : AttackData
{
    public float stackInterval;
    public int maxStack;

    public override ITowerAttack CreateAttack(Tower tower, List<ITowerEffect> effects)
    {
        return new BeamAttack(effects, stackInterval, maxStack);
    }
}