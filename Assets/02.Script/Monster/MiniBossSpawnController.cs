using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossSpawnController : MonoSingleton<MiniBossSpawnController>
{
    [Header("MiniBoss List")]
    [SerializeField] private List<MiniBossData> miniBossList;

    private Dictionary<MiniBossData, float> lastSpawnTime;

    protected override void Init()
    {
        lastSpawnTime = new Dictionary<MiniBossData, float>();

        foreach (var data in miniBossList)
            lastSpawnTime[data] = -999f;
    }

    public bool IsUnlocked(MiniBossData data)
    {
        return WaveManager.Instance.CurrentWave >= data.unlockWave;
    }

    public bool IsCooldownReady(MiniBossData data)
    {
        return Time.time >= lastSpawnTime[data] + data.cooldownTime;
    }

    public bool Spawn(MiniBossData data)
    {
        if (MonsterPoolManager.Instance.HasAliveMiniBoss())
        {
            return false;
        }

        if (!IsUnlocked(data))
        {
            return false;
        }

        if (!IsCooldownReady(data))
        {
            return false;
        }

        MonsterSpawn spawner = FindObjectOfType<MonsterSpawn>();
        spawner.SpawnSingle(data.monsterData);

        lastSpawnTime[data] = Time.time;

        var popup = FindObjectOfType<WavePopupUI>();
        if (popup != null)
            popup.Show($"MINI BOSS SPAWN");

        return true;
    }

    public float GetRemainCooldown(MiniBossData data)
    {
        float endTime = lastSpawnTime[data] + data.cooldownTime;
        return Mathf.Max(0f, endTime - Time.time);
    }
}
