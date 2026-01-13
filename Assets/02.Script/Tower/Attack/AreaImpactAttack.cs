using System.Collections.Generic;
using UnityEngine;

public class AreaImpactAttack : ITowerAttack
{
    private readonly float radius;
    private readonly LayerMask monsterLayer;
    private readonly List<ITowerEffect> effects;

    public AreaImpactAttack(float radius, LayerMask monsterLayer, List<ITowerEffect> effects)
    {
        this.radius = radius;
        this.monsterLayer = monsterLayer;
        this.effects = effects;
    }

    public void Execute(Monster target)
    {
        if (target == null) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(target.transform.position, radius, monsterLayer);

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
