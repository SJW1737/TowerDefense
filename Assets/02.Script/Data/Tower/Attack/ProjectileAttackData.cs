using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Attack/Projectile")]
public class ProjectileAttackData : AttackData
{
    public float projectileSpeed;
    public GameObject projectilePrefab;

    public override ITowerAttack CreateAttack(Tower tower, List<ITowerEffect> effects)
    {
        return new ProjectileAttack(tower, effects, projectileSpeed, projectilePrefab, tower.firePoint);
    }
}
