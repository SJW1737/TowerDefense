using System.Collections.Generic;
using UnityEngine;

public abstract class AttackData : ScriptableObject
{
    public abstract ITowerAttack CreateAttack(Tower tower, List<ITowerEffect> effects);
}

public interface ITickAttackData
{
    ITickableAttack CreateTickAttack(Tower tower, List<ITowerEffect> effects);
}