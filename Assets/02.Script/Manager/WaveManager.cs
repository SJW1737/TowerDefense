using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoSingleton<WaveManager>
{
    private MonsterSpawn monsterSpawn;

    private int currentWave = 1;
    private bool isRunning;

    public int CurrentWave => currentWave;

    public event Action<int> OnWaveChanged;

    protected override void Init()
    {
        monsterSpawn = FindObjectOfType<MonsterSpawn>();
    }

    public void StartGame()
    {
        if (isRunning) return;

        if (monsterSpawn == null)
            monsterSpawn = FindObjectOfType<MonsterSpawn>(true);

        if (monsterSpawn == null)
        {
            return;
        }

        isRunning = true;
        OnWaveChanged?.Invoke(currentWave);
        StartCoroutine(WaveLoop());
    }

    public void ResetWave()
    {
        StopAllCoroutines();
        currentWave = 1;
        isRunning = false;

        OnWaveChanged?.Invoke(currentWave);
    }

    private IEnumerator WaveLoop()
    {
        while (true)
        {
            if (monsterSpawn == null)
                yield break;

            Debug.Log($"Wave {currentWave} 시작");

            WaveData waveData = WaveGenerator.Generate(currentWave);

            if (monsterSpawn == null) yield break;
            monsterSpawn.StartWave(waveData);

            yield return WaitUntilAllMonsterDead();

            Debug.Log($"Wave {currentWave} 종료");

            currentWave++;
            OnWaveChanged?.Invoke(currentWave);

            yield return new WaitForSeconds(3f);
        }
    }

    private IEnumerator WaitUntilAllMonsterDead()
    {
        while (true)
        {
            if (monsterSpawn == null) yield break;

            var pool = MonsterPoolManager.Instance;
            if (pool == null) yield break;

            if (!monsterSpawn.IsWaveSpawning && MonsterPoolManager.Instance.AliveMonsterCount <= 0)
                break;

            yield return null;
        }
    }
}
