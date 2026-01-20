using UnityEngine;

public class AreaDotEffect : ITowerEffect, IUpgradeableEffect
{
    private readonly Tower tower;
    private readonly AreaDotCenterType centerType;
    private readonly float radius;
    private readonly float duration;
    private readonly float damageRatio;

    private int upgradeLevel;

    public AreaDotEffect( Tower tower, AreaDotCenterType centerType, float radius, float duration, float damageRatio)
    {
        this.tower = tower;
        this.centerType = centerType;
        this.radius = radius;
        this.duration = duration;
        this.damageRatio = damageRatio;
    }

    public void OnUpgrade(int level)
    {
        upgradeLevel = level;
    }

    public void Apply(Monster target)
    {
        tower.StartCoroutine(DoAreaDot(target));
    }

    private System.Collections.IEnumerator DoAreaDot(Monster target)
    {
        float time = 0f;
        float tickInterval = tower.data.GetAttackInterval(upgradeLevel);

        int baseDamage = tower.GetEffect<DamageEffect>().GetFinalDamage();

        int tickDamage = Mathf.RoundToInt(baseDamage * damageRatio);

        while (time < duration)
        {
            Vector2 center = centerType == AreaDotCenterType.Tower ? tower.transform.position : (target != null ? target.transform.position : (Vector2)tower.transform.position);

            Collider2D[] hits = Physics2D.OverlapCircleAll(center, radius, LayerMask.GetMask("Monster"));

            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out Monster monster))
                {
                    monster.TakeDamage(tickDamage);
                }
            }

            yield return new WaitForSeconds(tickInterval);
            time += tickInterval;
        }
    }
}
