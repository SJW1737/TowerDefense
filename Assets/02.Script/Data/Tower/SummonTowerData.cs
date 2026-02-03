using UnityEngine;

[CreateAssetMenu(menuName = "Tower/SummonTowerData")]
public class SummonTowerData : TowerData
{
    public GameObject summonPrefab;
    public int baseSummonCount;

    public int GetSummonCount(int upgradeCount)
    {
        return baseSummonCount + upgradeCount;
    }
}
