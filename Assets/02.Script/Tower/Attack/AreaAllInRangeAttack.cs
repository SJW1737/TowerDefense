using System.Collections.Generic;
using UnityEngine;

public class AreaAllInRangeAttack : ITowerAttack
{
    private readonly float range;
    private readonly LayerMask monsterLayer;
    private readonly List<ITowerEffect> effects;

    public AreaAllInRangeAttack(float range, LayerMask monsterLayer, List<ITowerEffect> effects)
    {
        this.range = range;
        this.monsterLayer = monsterLayer;
        this.effects = effects;
    }

    public void Execute(Monster _)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(Vector2.zero, range, monsterLayer);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Monster monster))
            {
                foreach (var effect in effects)
                    effect.Apply(monster);
            }
        }
    }
}
