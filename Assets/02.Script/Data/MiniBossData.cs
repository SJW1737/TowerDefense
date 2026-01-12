using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MiniBoss/MiniBossData")]
public class MiniBossData : ScriptableObject
{
    [Header("Info")]
    public string bossName;

    public MonsterData monsterData;

    [Header("Unlock")]
    public int unlockWave;

    [Header("Cooldown")]
    public float cooldownTime;
}
