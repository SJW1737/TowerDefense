using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RelicManager : MonoSingleton<RelicManager>
{
    [SerializeField] private List<RelicData> allRelicData;

    private List<OwnedRelic> relics = new();

    public static bool IsReady => instance != null;

    public event Action OnRelicChanged;

    public static RelicManager TraceInstance
    {
        get
        {
            return Instance;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    protected override void Init()
    {
        if (SaveManager.Instance == null || SaveManager.Instance.Data == null)
        {
            return;
        }

        if (allRelicData == null || allRelicData.Count == 0)
        {
            return;
        }

        LoadRelicsFromSave();
    }

    private void LoadRelicsFromSave()
    {
        relics.Clear();

        var saveRelics = SaveManager.Instance.Data.relics;

        foreach (var relicData in allRelicData)
        {
            int level = 0;
            int piece = 0;

            var saved = saveRelics.Find(r => r.relicId == relicData.relicId);
            if (saved != null)
            {
                level = saved.level;
                piece = saved.piece;
            }

            relics.Add(new OwnedRelic
            {
                data = relicData,
                level = level,
                piece = piece
            });
        }
    }

    public bool AddRelicPiece(RelicData data)
    {
        var relic = GetOwnedRelic(data);
        if (relic == null || relic.IsMax)
            return false;

        if (relic.level == 0)
        {
            relic.level = 1;
        }
        else
        {
            relic.piece++;
        }

        SaveRelic(relic);
        OnRelicChanged?.Invoke();
        return true;
    }

    public bool TryUpgrade(RelicData data)
    {
        var relic = GetOwnedRelic(data);
        if (relic == null || !relic.CanUpgrade)
            return false;

        relic.level++;
        relic.piece--;

        SaveRelic(relic);
        OnRelicChanged?.Invoke();
        return true;
    }

    private void SaveRelic(OwnedRelic relic)
    {
        var saveList = SaveManager.Instance.Data.relics;

        var saved = saveList.Find(r => r.relicId == relic.data.relicId);
        if (saved == null)
        {
            saveList.Add(new RelicSaveData
            {
                relicId = relic.data.relicId,
                level = relic.level,
                piece = relic.piece
            });
        }
        else
        {
            saved.level = relic.level;
            saved.piece = relic.piece;
        }

        SaveManager.Instance.Save();
    }

    public float GetValue(RelicEffectType type)
    {
        float value = 0f;

        foreach (var relic in relics)
        {
            if (relic.data.effectType == type && relic.level > 0)
            {
                value += relic.GetValue();
            }
                
        }

        return value;
    }

    public OwnedRelic GetOwnedRelic(RelicData data)
    {
        return relics.Find(r => r.data.relicId == data.relicId);
    }

    public IReadOnlyList<OwnedRelic> GetAllRelics()
    {
        return relics;
    }
}
