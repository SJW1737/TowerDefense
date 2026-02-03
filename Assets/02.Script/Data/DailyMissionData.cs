using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DailyMission/DailyMissionData")]
public class DailyMissionData : ScriptableObject
{
    public string id;
    public string title;
    public string description;

    public DailyMissionType type;
    public int targetCount;

    public int rewardDiamond;
}
