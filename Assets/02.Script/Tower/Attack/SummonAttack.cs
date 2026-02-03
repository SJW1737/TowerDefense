using System.Collections.Generic;
using UnityEngine;

public class SummonAttack : ITowerAttack
{
    private Tower tower;
    private SummonTowerData data;

    private List<SummonUnit> activeSummons = new();

    public SummonAttack(Tower tower, SummonTowerData data)
    {
        this.tower = tower;
        this.data = data;
    }

    public void Execute(Monster target)
    {
        int maxSummon = data.GetSummonCount(tower.UpgradeCount);

        activeSummons.RemoveAll(s => s == null);

        if (activeSummons.Count >= maxSummon)
            return;

        GameObject summonObj =
            GameObject.Instantiate(data.summonPrefab, tower.transform.position, Quaternion.identity);

        SummonUnit summon = summonObj.GetComponent<SummonUnit>();
        summon.Initialize(tower, target);

        activeSummons.Add(summon);
    }
}
