using UnityEngine;

[CreateAssetMenu(menuName = "Tower/BombTowerData")]
public class BombTowerData : TowerData
{
    [Header("ÆøÅº Àü¿ë ¼öÄ¡")]
    public float baseExplosionRadius;
    public float radiusPerUpgrade;

    public float GetExplosionRadius(int upgradeCount)
    {
        return baseExplosionRadius + radiusPerUpgrade * upgradeCount;
    }
}
