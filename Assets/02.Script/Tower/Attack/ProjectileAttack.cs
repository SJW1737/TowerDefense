using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : ITowerAttack
{
    private Tower ownerTower;
    private List<ITowerEffect> effects;
    private float projectileSpeed;
    private GameObject projectilePrefab;
    private Transform firePoint;

    public ProjectileAttack(Tower ownerTower, List<ITowerEffect> effects, float projectileSpeed, GameObject projectilePrefab, Transform firePoint)
    {
        this.ownerTower = ownerTower;
        this.effects = effects;
        this.projectileSpeed = projectileSpeed;
        this.projectilePrefab = projectilePrefab;
        this.firePoint = firePoint;
    }

    public void Execute(Monster target)
    {
        if (target == null) return;

        GameObject projObj = Object.Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // ∆¯≈∫ ≈ıªÁ√º
        if (projObj.TryGetComponent(out BombProjectile bomb))
        {
            if (ownerTower.data is BombTowerData bombData)
            {
                float radius = bombData.GetExplosionRadius(ownerTower.UpgradeCount);

                bomb.Init(target.transform.position, projectileSpeed, radius, effects);
            }
            else
            {
                Debug.LogError("BombProjectile¿Œµ• BombTowerData∞° æ∆¥‘");
            }

            return;
        }

        // ±‚∫ª ≈ıªÁ√º
        if (projObj.TryGetComponent(out Projectile projectile))
        {
            projectile.Init(target, projectileSpeed, effects);
            return;
        }
    }
}
