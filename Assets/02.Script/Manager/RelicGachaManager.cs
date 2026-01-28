using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        if (!RelicManager.Instance.GetAllRelics().Any(r => !r.IsMax))
        {
            return false;
        }

        if (!SaveManager.Instance.SpendDiamond(gachaCost))
            return false;

        RelicData result = Draw();

        if (result == null)
        {
            return false;
        }

        RelicManager.Instance.AddRelicPiece(result);
        var owned = RelicManager.Instance.GetOwnedRelic(result);
        RelicGachaResultUI.Instance.Open(owned);
        return true;
    }

    private RelicData Draw()
    {
        var candidates = new List<RelicGachaEntry>();

        foreach (var e in gachaPool)
        {
            var owned = RelicManager.Instance.GetOwnedRelic(e.relicData);

            if (owned != null && !owned.IsMax)
            {
                candidates.Add(e);
            }
        }

        if (candidates.Count == 0)
        {
            return null;
        }

        int total = candidates.Sum(e => e.weight);
        int rand = Random.Range(0, total);

        int acc = 0;
        foreach (var e in candidates)
        {
            acc += e.weight;
            if (rand < acc)
            {
                return e.relicData;
            }
        }

        return null;
    }
}
