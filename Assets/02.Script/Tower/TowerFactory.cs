using System.Collections.Generic;

public static class TowerFactory
{
    public static void SetupTower(Tower tower)
    {
        TowerData data = tower.data;

        List<ITowerEffect> effects = CreateEffects(data);
        ITowerAttack attack = CreateAttack(tower, data, effects);

        tower.SetAttack(attack);
    }

    private static ITowerAttack CreateAttack(Tower tower, TowerData data, List<ITowerEffect> effects)
    {
        switch (data.attackType)
        {
            case AttackType.ProjectileSingle:
                return new ProjectileAttack(effects, data.projectileSpeed, tower.projectilePrefab, tower.firePoint);

            case AttackType.ProjectileAOE:
                return new ExplosionAttack(effects);

            case AttackType.Beam:
                return new BeamAttack(effects);

            default:
                return null;
        }
    }

    private static List<ITowerEffect> CreateEffects(TowerData data)
    {
        List<ITowerEffect> effects = new List<ITowerEffect>();
        
        // 데미지
        if (data.damage > 0)
        {
            effects.Add(new DamageEffect(data.damage));
        }

        // 슬로우
        if (data.slowRatio > 0f)
        {
            effects.Add(new SlowEffect(data.slowRatio, data.slowDuration));
        }

        return effects;
    }
}