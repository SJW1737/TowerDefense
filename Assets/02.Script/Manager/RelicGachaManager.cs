using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicGachaManager : MonoSingleton<RelicGachaManager>
{
    [SerializeField] private int gachaCost = 100;
    [SerializeField] private List<RelicGachaEntry> gachaPool;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public bool TryGacha()
    {
        if (!SaveManager.Instance.SpendDiamond(gachaCost))
            return false;

        RelicData result = Draw();
        RelicManager.Instance.AddRelicLevel(result);
        return true;
    }

    private RelicData Draw()
    {
        int totalWeight = 0;
        foreach (var e in gachaPool)
            totalWeight += e.weight;

        int rand = Random.Range(0, totalWeight);
        int acc = 0;

        foreach (var e in gachaPool)
        {
            acc += e.weight;
            if (rand < acc)
                return e.relicData;
        }

        return gachaPool[0].relicData;
    }
}
