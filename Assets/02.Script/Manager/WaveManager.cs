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

        maxClearedWave = 0;

        OnWaveChanged?.Invoke(currentWave);

        StartCoroutine(WaveLoop());
    }

    public void ResetWave()
    {
        StopAllCoroutines();
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

            Debug.Log($"Wave {currentWave} 시작");

            WaveData waveData = WaveGenerator.Generate(currentWave);
            monsterSpawn.StartWave(waveData);

            ShowWavePopup(currentWave);

            yield return WaitUntilAllMonsterDead();

            Debug.Log($"Wave {currentWave} 종료");

            maxClearedWave = currentWave;

            currentWave++;
            OnWaveChanged?.Invoke(currentWave);

            yield return new WaitForSeconds(3f);
        }
    }

    private void ShowWavePopup(int wave)
    {
        var popup = FindObjectOfType<WavePopupUI>();
        if (popup == null) return;

        if (wave % 10 == 0)
            popup.Show("BOSS WAVE");
        else
            popup.Show($"{wave} WAVE");
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
