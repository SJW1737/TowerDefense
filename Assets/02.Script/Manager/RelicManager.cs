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
        LoadRelicsFromSave();
    }

    private void LoadRelicsFromSave()
    {
        relics.Clear();

        var saveRelics = SaveManager.Instance.Data.relics;

        foreach (var relicData in allRelicData)
        {
            int level = 0;

            var saved = saveRelics.Find(r => r.relicId == relicData.relicId);
            if (saved != null)
                level = saved.level;

            relics.Add(new OwnedRelic
            {
                data = relicData,
                level = level
            });
        }
    }

    public void AddRelicLevel(RelicData relicData)
    {
        var relic = relics.Find(r => r.data == relicData);
        if (relic == null || relic.level >= relic.data.maxLevel)
            return;

        relic.level++;
        SaveRelic(relic);

        OnRelicChanged?.Invoke();
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
                level = relic.level
            });
        }
        else
        {
            saved.level = relic.level;
        }

        SaveManager.Instance.Save();
    }

    public float GetValue(RelicEffectType type)
    {
        float value = 0f;

        foreach (var relic in relics)
        {
            if (relic.data.effectType == type)
            {
                value += relic.level * relic.data.valuePerLevel;
            }
                
        }

        return value;
    }

    public IReadOnlyList<OwnedRelic> GetAllRelics()
    {
        return relics;
    }
}
