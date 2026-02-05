using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Achievement/AchievementData")]
public class AchievementData : ScriptableObject
{
    public string id;
    public string title;
    public string description;

    public AchievementType type;

    public int stepCount;       // n»∏ ¥Á 
    public int rewardDiamond;
}
