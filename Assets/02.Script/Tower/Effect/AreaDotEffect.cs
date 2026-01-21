using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDotEffect : ITowerEffect, IUpgradeableEffect
{
    private readonly Tower tower;
    private readonly float duration;
    private readonly float damageRatio;

    private int upgradeLevel;

    private readonly HashSet<Monster> activeTargets = new();

    public AreaDotEffect( Tower tower, float duration, float damageRatio)
    {
        this.tower = tower;
        this.duration = duration;
        this.damageRatio = damageRatio;
    }

    public void OnUpgrade(int level)
    {
        upgradeLevel = level;
    }

    public void Apply(Monster target)
    {
        if (target == null)
            return;

        if (activeTargets.Contains(target))
            return; // 이미 DOT 중이면 재시작 안 함

        activeTargets.Add(target);
        tower.StartCoroutine(DoAreaDot(target));
    }

    private IEnumerator DoAreaDot(Monster target)
    {
        float time = 0f;
        float tickInterval = tower.data.GetAttackInterval(upgradeLevel);

        int baseDamage = tower.GetEffect<DamageEffect>().GetFinalDamage();
        int tickDamage = Mathf.RoundToInt(baseDamage * damageRatio);

        while (time < duration && target != null)
        {
            target.TakeDamage(tickDamage);

            yield return new WaitForSeconds(tickInterval);
            time += tickInterval;
        }

        activeTargets.Remove(target);
    }
}
