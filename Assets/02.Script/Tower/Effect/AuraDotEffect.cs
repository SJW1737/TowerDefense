using UnityEngine;

public class AuraDotEffect : ITowerEffect
{
    private readonly DamageEffect damageEffect;
    private readonly float radius;           // 영향 반경
    private readonly float damageRatio;      // 타워 공격력 대비 비율 (ex: 0.5f)
    private readonly float duration;         // DOT 지속 시간
    private readonly float tickInterval;     // DOT 틱 간격
    private readonly LayerMask monsterLayer;

    public AuraDotEffect(DamageEffect damageEffect, float radius, float damageRatio, float duration, float tickInterval, LayerMask monsterLayer)
    {
        this.damageEffect = damageEffect;
        this.radius = radius;
        this.damageRatio = damageRatio;
        this.duration = duration;
        this.tickInterval = tickInterval;
        this.monsterLayer = monsterLayer;
    }

    /// <summary>
    /// center = 공격을 "맞은" 몬스터
    /// </summary>
    public void Apply(Monster center)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(center.transform.position, radius, monsterLayer);

        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent(out Monster monster))
                continue;

            // 타워 기준 공격력 -> DOT 계산
            float damagePerTick = damageEffect.GetFinalDamage() * damageRatio;

            monster.ApplyDot(damagePerTick, duration, tickInterval);
        }
    }
}
