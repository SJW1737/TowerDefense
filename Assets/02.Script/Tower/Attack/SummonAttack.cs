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

        int index = activeSummons.Count; // 슬롯 인덱스

        GameObject summonObj = GameObject.Instantiate(data.summonPrefab, tower.transform.position, Quaternion.identity);

        SummonUnit summon = summonObj.GetComponent<SummonUnit>();
        summon.Initialize(tower, target);

        // 일자 배치 계산
        float spacing = 0.6f;
        Vector3 basePos = tower.transform.position + Vector3.up * 0.8f;

        float offset = (index - (maxSummon - 1) / 2f) * spacing;
        Vector3 standbyPos = basePos + new Vector3(offset, 0f, 0f);

        summon.SetSlot(index, standbyPos);

        activeSummons.Add(summon);
    }
}
