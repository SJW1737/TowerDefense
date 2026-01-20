using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAreaAttack : ITickableAttack
{
    private readonly Tower tower;
    private readonly List<ITowerEffect> effects;
    private readonly GameObject firePrefab;

    public FireAreaAttack(Tower tower, List<ITowerEffect> effects, GameObject firePrefab)
    {
        this.tower = tower;
        this.effects = effects;
        this.firePrefab = firePrefab;
    }

    public void Tick(float deltaTime)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(tower.transform.position, tower.data.range, LayerMask.GetMask("Monster"));

        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent(out Monster monster))
                continue;

            foreach (var effect in effects)
                effect.Apply(monster);
        }
    }
}
