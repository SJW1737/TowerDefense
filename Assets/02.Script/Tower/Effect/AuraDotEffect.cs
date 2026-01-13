using UnityEngine;

public class AuraDotEffect : ITowerEffect
{
    private readonly float radius;           // 영향 반경
    private readonly float damageRatio;      // 타워 공격력 대비 비율 (ex: 0.5f)
    private readonly float duration;         // DOT 지속 시간
    private readonly float tickInterval;     // DOT 틱 간격
    private readonly LayerMask monsterLayer;

    private readonly DamageEffect damageSource;

    public AuraDotEffect(float radius, float damageRatio, float duration, float tickInterval, LayerMask monsterLayer, DamageEffect damageSource)
    {
        this.radius = radius;
        this.damageRatio = damageRatio;
        this.duration = duration;
        this.tickInterval = tickInterval;
        this.monsterLayer = monsterLayer;
        this.damageSource = damageSource;
    }

    /// <summary>
    /// center = 공격을 "맞은" 몬스터
    /// </summary>
    public void Apply(Monster center)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(center.transform.position, radius,monsterLayer);

        // 타워 기준 공격력 -> DOT 계산
        float baseAttackDamage = damageSource.GetFinalDamage(0);
        float damagePerTick = baseAttackDamage * damageRatio;

        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent(out Monster monster))
                continue;

            monster.ApplyDot(damagePerTick, duration, tickInterval);
        }
    }
}
