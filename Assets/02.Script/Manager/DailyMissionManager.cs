using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyMissionManager : MonoSingleton<DailyMissionManager>
{
    [Header("Mission Data")]
    [SerializeField] private List<DailyMissionData> missionDatas;

    private Dictionary<string, DailyMissionSaveData> missionDict;
    public IReadOnlyList<DailyMissionData> MissionDatas => missionDatas;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    protected override void Init()
    {
        EnsureSaveDataReady();
        CheckDailyReset();
        InitMissionData();
    }

    private void EnsureSaveDataReady()
    {
        var saveManager = SaveManager.Instance;
        if (saveManager.Data == null)
            saveManager.Load();
    }

    private void InitMissionData()
    {
        missionDict = new Dictionary<string, DailyMissionSaveData>();

        SaveManager saveManager = SaveManager.Instance;
        if (saveManager.Data == null)
        {
            saveManager.Load();
        }

        SaveData saveData = SaveManager.Instance.Data;

        bool isDirty = false;

        foreach (var data in missionDatas)
        {
            var saved = saveData.dailyMissions.Find(x => x.missionId == data.id);

            if (saved == null)
            {
                saved = new DailyMissionSaveData
                {
                    missionId = data.id,
                    currentCount = 0,
                    isCompleted = false,
                    rewardClaimed = false
                };

                saveData.dailyMissions.Add(saved);
                isDirty = true;
            }

            missionDict[data.id] = saved;
        }

        if (isDirty)
        {
            saveManager.Save();
        }
    }

    //리셋
    private void CheckDailyReset()
    {
        SaveData saveData = SaveManager.Instance.Data;

        string today = DateTime.Now.ToString("yyyyMMdd");

        if (string.IsNullOrEmpty(saveData.dailyMissionDate))
        {
            saveData.dailyMissionDate = today;
            SaveManager.Instance.Save();
            return;
        }

        if (saveData.dailyMissionDate != today)
        {
            ResetAll();
            saveData.dailyMissionDate = today;
            SaveManager.Instance.Save();
        }
    }

    private void ResetAll()
    {
        SaveData saveData = SaveManager.Instance.Data;

        foreach (var mission in saveData.dailyMissions)
        {
            mission.currentCount = 0;
            mission.isCompleted = false;
            mission.rewardClaimed = false;
        }
    }

    public void AddProgress(DailyMissionType type, int amount = 1)
    {
        foreach (var data in missionDatas)
        {
            if (data.type != type)
                continue;

            var save = missionDict[data.id];

            if (save.isCompleted)
                continue;

            save.currentCount += amount;

            if (save.currentCount >= data.targetCount)
                save.isCompleted = true;
        }

        SaveManager.Instance.Save();
    }

    //보상
    public bool ClaimReward(string missionId)
    {
        if (!missionDict.TryGetValue(missionId, out var save))
            return false;

        if (!save.isCompleted || save.rewardClaimed)
            return false;

        var data = missionDatas.Find(x => x.id == missionId);
        if (data == null)
            return false;

        SaveManager.Instance.AddDiamond(data.rewardDiamond);

        save.rewardClaimed = true;
        SaveManager.Instance.Save();
        return true;
    }

    //UI
    public DailyMissionSaveData GetMissionSaveData(string missionId)
    {
        missionDict.TryGetValue(missionId, out var data);
        return data;
    }
}
