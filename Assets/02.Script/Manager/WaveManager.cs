using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoSingleton<WaveManager>
{
    private MonsterSpawn monsterSpawn;

    private int currentWave = 1;
    private bool isRunning;

    public int CurrentWave => currentWave;

    protected override void Init()
    {
        monsterSpawn = FindObjectOfType<MonsterSpawn>();
    }

    public void StartGame()
    {
        if (isRunning) return;
        isRunning = true;

        StartCoroutine(WaveLoop());
    }

    public void ResetWave()
    {
        StopAllCoroutines();
        currentWave = 1;
        isRunning = false;
    }

    private IEnumerator WaveLoop()
    {
        while (true)
        {
            Debug.Log($"Wave {currentWave} 시작");

            WaveData waveData = WaveGenerator.Generate(currentWave);

            monsterSpawn.StartWave(waveData);

            yield return WaitUntilAllMonsterDead();

            Debug.Log($"Wave {currentWave} 종료");

            currentWave++;

            yield return new WaitForSeconds(3f);
        }
    }

    private IEnumerator WaitUntilAllMonsterDead()
    {
        while (monsterSpawn.IsWaveSpawning || MonsterPoolManager.Instance.AliveMonsterCount > 0)
        {
            yield return null;
        }
    }
}
