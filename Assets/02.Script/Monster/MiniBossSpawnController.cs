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

    public float GetRemainCooldown(MiniBossData data)
    {
        float end = lastSpawnTime[data] + data.cooldownTime;
        return Mathf.Max(0, end - Time.time);
    }

    public void Spawn(MiniBossData data)
    {
        if (MonsterPoolManager.Instance.HasAliveMiniBoss())
        {
            return;
        }

        if (!IsUnlocked(data))
        {
            return;
        }

        if (!IsCooldownReady(data))
        {
            return;
        }

        MonsterSpawn spawner = FindObjectOfType<MonsterSpawn>();
        spawner.SpawnSingle(data.monsterData);

        lastSpawnTime[data] = Time.time;
    }
}
