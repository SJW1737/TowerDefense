using System.Collections.Generic;
using UnityEngine;

public class FireAreaAttack : ITickableAttack
{
    private readonly Tower tower;
    private readonly List<ITowerEffect> effects;

    private float tickTimer;

    public FireAreaAttack(Tower tower, List<ITowerEffect> effects)
    {
        this.tower = tower;
        this.effects = effects;
    }

    public void Tick(float deltaTime)
    {
        tickTimer += deltaTime;

        float interval = tower.data.GetAttackInterval(tower.UpgradeCount);
        if (tickTimer < interval)
            return;

        tickTimer = 0f;

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            tower.transform.position,
            tower.data.range,
            LayerMask.GetMask("Monster")
        );

        if (hits.Length == 0)
            return;

        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent(out Monster monster))
                continue;

            foreach (var effect in effects)
                effect.Apply(monster);
        }
    }
}
