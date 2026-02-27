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

    private int maxClearedWave = 0;

    [Header("Wave Settings")]
    [SerializeField] private float prepareTime = 3f;

    [Header("UI")]
    [SerializeField] private WavePopupUI wavePopupUI;
    [SerializeField] private WaveDelayUI waveDelayUI;

    public int CurrentWave => currentWave;

    public event Action<int> OnWaveChanged;
    protected override void Init()
    {
        monsterSpawn = FindObjectOfType<MonsterSpawn>();
    }

    public void StartGame()
    {
        if (isRunning) return;

        InGameSession.Instance.ResetSession();

        if (monsterSpawn == null)
            monsterSpawn = FindObjectOfType<MonsterSpawn>(true);

        if (monsterSpawn == null)
        {
            return;
        }

        isRunning = true;
        currentWave = 1;
        maxClearedWave = 0;

        OnWaveChanged?.Invoke(currentWave);

        StartCoroutine(WaveLoop());
    }

    public void ResetWave()
    {
        StopAllCoroutines();

        InGameSession.Instance.ResetSession();

        currentWave = 1;
        isRunning = false;

        maxClearedWave = 0;

        OnWaveChanged?.Invoke(currentWave);
    }

    private IEnumerator WaveLoop()
    {
        while (true)
        {
            if (monsterSpawn == null)
                yield break;

            bool isBoss = currentWave % 10 == 0;

            if (wavePopupUI != null)
            {
                string text = isBoss ? "보스 웨이브" : $"{currentWave} 웨이브";
                wavePopupUI.Show(text, isBoss);

                yield return new WaitForSeconds(isBoss ? 0.8f : 1f);
            }

            if (waveDelayUI != null)
                waveDelayUI.StartCountdown(prepareTime);

            yield return new WaitForSeconds(prepareTime);

            DifficultyManager.Instance.UpdateDifficulty(currentWave);

            Debug.Log($"Wave {currentWave} 시작");

            WaveData waveData = WaveGenerator.Generate(currentWave);
            monsterSpawn.StartWave(waveData);

            yield return WaitUntilAllMonsterDead();

            Debug.Log($"Wave {currentWave} 종료");

            maxClearedWave = currentWave;

            InGameSession.Instance.clearWave++;

            currentWave++;
            OnWaveChanged?.Invoke(currentWave);
        }
    }

    private IEnumerator WaitUntilAllMonsterDead()
    {
        while (true)
        {
            if (!monsterSpawn.IsWaveSpawning && MonsterPoolManager.Instance.AliveMonsterCount <= 0)
                break;

            yield return null;
        }
    }

    //실제 지급 계산
    public void GrantWaveClearDiamond()
    {
        if (maxClearedWave < 10)
            return;

        int checkpointWave = (maxClearedWave / 10) * 10;

        int rewardDiamond = (checkpointWave / 10) * 50;

        SaveManager.Instance.AddDiamond(rewardDiamond);
    }

    //UI용
    public int GetWaveClearDiamondReward()
    {
        if (maxClearedWave < 10)
            return 0;

        int checkpointWave = (maxClearedWave / 10) * 10;
        return (checkpointWave / 10) * 50;
    }
}
