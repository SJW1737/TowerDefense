using System.Collections.Generic;
using UnityEngine;

public class FireAreaAttack : ITickableAttack
{
    private readonly Tower tower;
    private float tickTimer;

    private const float burnDuration = 3f;   // 3초 지속
    private const float burnRatio = 0.3f;    // 공격력의 30%

    public FireAreaAttack(Tower tower)
    {
        this.tower = tower;
    }

    public void Tick(float deltaTime)
    {
        tickTimer += deltaTime;

        float interval = tower.data.GetAttackInterval(tower.UpgradeCount);
        if (tickTimer < interval)
            return;

        tickTimer = 0f;

        Collider2D[] hits = Physics2D.OverlapCircleAll(tower.transform.position, tower.data.range, LayerMask.GetMask("Monster"));

        if (hits.Length == 0)
            return;

        int baseDamage = tower.GetEffect<DamageEffect>().GetFinalDamage();
        int burnDamage = Mathf.Max(1, Mathf.RoundToInt(baseDamage * burnRatio));

        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent(out Monster monster))
                continue;

            // 즉시 피해
            monster.TakeDamage(baseDamage);

            // DOT 피해
            var burn = monster.GetComponent<MonsterBurn>();
            if (burn != null)
                burn.ApplyBurn(burnDamage, burnDuration, interval);
        }
    }
}
