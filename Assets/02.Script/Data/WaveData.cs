using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class WaveMonster
{
    public MonsterType type;
    public int count;
}

[System.Serializable]
public class WaveData
{
    public List<WaveMonster> monsters;
    public float spawnInterval;
}
