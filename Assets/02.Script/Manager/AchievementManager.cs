using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoSingleton<AchievementManager>
{
    [Header("Achievement Data")]
    [SerializeField] 
    private List<AchievementData> achievementDatas;

    private Dictionary<string, AchievementSaveData> achievementDict;

    public IReadOnlyList<AchievementData> AchievementDatas => achievementDatas;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    protected override void Init()
    {
        EnsureSaveDataReady();
        InitAchievementData();
    }

    private void EnsureSaveDataReady()
    {
        if (SaveManager.Instance.Data == null)
            SaveManager.Instance.Load();
    }

    private void InitAchievementData()
    {
        achievementDict = new Dictionary<string, AchievementSaveData>();

        SaveData saveData = SaveManager.Instance.Data;
        bool isDirty = false;

        foreach (var data in achievementDatas)
        {
            var saved = saveData.achievements.Find(x => x.achievementId == data.id);

            if (saved == null)
            {
                saved = new AchievementSaveData
                {
                    achievementId = data.id,
                    totalCount = 0,
                    rewardedStep = 0
                };

                saveData.achievements.Add(saved);
                isDirty = true;
            }

            achievementDict[data.id] = saved;
        }

        if (isDirty)
            SaveManager.Instance.Save();
    }

    //업적 진행도 추가
    public void AddProgress(AchievementType type, int amount = 1)
    {
        foreach (var data in achievementDatas)
        {
            if (data.type != type)
                continue;

            if (data.stepCount <= 0)
                continue;

            if (!achievementDict.TryGetValue(data.id, out var save))
                continue;

            save.totalCount += amount;

            save.claimableStep = save.totalCount / data.stepCount;
        }

        SaveManager.Instance.Save();
    }

    public bool ClaimReward(string achievementId)
    {
        if (!achievementDict.TryGetValue(achievementId, out var save))
            return false;

        if (save.claimableStep <= 0)
            return false;

        var data = achievementDatas.Find(x => x.id == achievementId);

        if (data == null)
            return false;

        int reward = save.claimableStep * data.rewardDiamond;

        SaveManager.Instance.AddDiamond(reward);

        save.totalCount %= data.stepCount;

        save.claimableStep = 0;
        SaveManager.Instance.Save();

        return true;
    }

    public AchievementSaveData GetSaveData(string achievementId)
    {
        achievementDict.TryGetValue(achievementId, out var data);
        return data;
    }
}
