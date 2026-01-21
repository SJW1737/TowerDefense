using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Attack/FireArea")]
public class FireAreaAttackData : AttackData, ITickAttackData
{
    public override ITowerAttack CreateAttack(Tower tower, List<ITowerEffect> effects)
    {
        return null; // 단일 공격 없음
    }

    public ITickableAttack CreateTickAttack(Tower tower, List<ITowerEffect> effects)
    {
        return new FireAreaAttack(tower, effects);
    }
}
