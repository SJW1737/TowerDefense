using System;
using System.Collections.Generic;

[Serializable]
public class RelicSaveData
{
    public string relicId;
    public int level;
    public int piece;
}

[Serializable]
public class SaveData
{
    public bool startGoldApplied;

    public int diamond;

    public List<RelicSaveData> relics = new();

    public string dailyMissionDate;
    public List<DailyMissionSaveData> dailyMissions = new();

    public List<AchievementSaveData> achievements = new();
}

[Serializable]
public class DailyMissionSaveData
{
    public string missionId;
    public int currentCount;
    public bool isCompleted;
    public bool rewardClaimed;
}

[Serializable]
public class AchievementSaveData
{
    public string achievementId;

    public int totalCount;      //누적 횟수
    public int rewardedStep;    //중복 보상 
    public int claimableStep;   //받을 수 있는 단계
}