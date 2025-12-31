using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoSingleton<WaveManager>
{
    public List<WaveData> waves;

    private int currentWaveIndex = 0;
    private MonsterSpawn monsterSpawn;

    protected override void Init()
    {
        monsterSpawn = FindObjectOfType<MonsterSpawn>();
    }

    public void StartFirstWave()
    {
        currentWaveIndex = 0;
        StartCurrentWave();
    }

    public void StartCurrentWave()
    {
        if (currentWaveIndex >= waves.Count)
        {
            Debug.Log("모든 웨이브 종료");
            return;
        }

        WaveData wave = waves[currentWaveIndex];
        monsterSpawn.StartWave(wave);
    }

    public void StartNextWave()
    {
        currentWaveIndex++;
        StartCurrentWave();
    }
}
