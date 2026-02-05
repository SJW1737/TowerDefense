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

        if (activeSummons.Count >= maxSummon)
            return;

        GameObject summonObj = GameObject.Instantiate(data.summonPrefab, tower.transform.position, Quaternion.identity);

        SummonUnit summon = summonObj.GetComponent<SummonUnit>();
        summon.Initialize(tower, target);

        activeSummons.Add(summon);

        RepositionSummons();
    }

    private void RepositionSummons()
    {
        float spacing = 0.4f;
        Vector3 basePos = tower.transform.position + Vector3.up * 0.8f;

        int count = activeSummons.Count;

        for (int i = 0; i < count; i++)
        {
            float offset = (i - (count - 1) / 2f) * spacing;
            Vector3 pos = basePos + new Vector3(offset, 0f, 0f);

            activeSummons[i].SetSlot(i, pos);
        }
    }
}
