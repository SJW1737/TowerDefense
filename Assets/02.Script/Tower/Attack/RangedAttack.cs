using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : ITowerAttack
{
    private List<ITowerEffect> effects;
    private float projectileSpeed;
    private GameObject projectilePrefab;
    private Transform firePoint;

    public RangedAttack(List<ITowerEffect> effects, float projectileSpeed, GameObject projectilePrefab, Transform firePoint)
    {
        this.effects = effects;
        this.projectileSpeed = projectileSpeed;
        this.projectilePrefab = projectilePrefab;
        this.firePoint = firePoint;
    }

    public void Execute(Monster target)
    {
        if (target == null) return;

        GameObject projObj = Object.Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        Projectile projectile = projObj.GetComponent<Projectile>();
        projectile.Init(target, projectileSpeed, effects);
    }
}
