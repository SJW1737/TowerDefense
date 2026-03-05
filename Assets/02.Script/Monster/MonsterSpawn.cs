using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public bool IsWaveSpawning { get; private set; }

    public void StartWave(WaveData waveData, int currentWave)
    {
        StartCoroutine(SpawnWave(waveData, currentWave));
    }

    IEnumerator SpawnWave(WaveData waveData, int currentWave)
    {
        IsWaveSpawning = true;

        Node startNode = GridManager.Instance.startNode;

        Vector3 spawnPos = new Vector3(startNode.x + 0.5f, startNode.y + 0.5f, 0) + GridManager.Instance.worldOffset;

        int waveTier = (currentWave - 1) / 10;
        int spawnBatch = Mathf.Max(1, (int)Mathf.Pow(2, waveTier));

        foreach (var monster in waveData.monsters)
        {
            int spawned = 0;

            while (spawned < monster.count)
            {
                int batchCount = Mathf.Min(spawnBatch, monster.count - spawned);

                for (int i = 0; i < batchCount; i++)
                {
                    if (monster.type == MonsterType.Boss && spawned == 0)
                    {
                        SoundManager.Instance.PlaySFX("BossAppear");
                    }

                    Debug.Log(monster.type + " spawn");

                    MonsterPoolManager.Instance.GetMonster(monster.type, spawnPos);

                    spawned++;

                    if (i < batchCount - 1)
                    {
                        yield return new WaitForSeconds(Random.Range(0.2f, 0.5f));

                    }
                }

                if (spawned < monster.count)
                {
                    yield return new WaitForSeconds(waveData.spawnInterval);
                }
            }
            yield return new WaitForSeconds(waveData.spawnInterval);
        }
        IsWaveSpawning = false;
    }

    public void SpawnSingle(MonsterData monsterData)
    {
        if (monsterData == null)
        {
            return;
        }

        Node startNode = GridManager.Instance.startNode;
        Vector3 spawnPos = new Vector3(startNode.x + 0.5f, startNode.y + 0.5f, 0) + GridManager.Instance.worldOffset;

        MonsterPoolManager.Instance.GetMonster(monsterData.monsterType, spawnPos);
    }
}
