using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public void StartWave(WaveData waveData)
    {
        StartCoroutine(SpawnWave(waveData));
    }

    IEnumerator SpawnWave(WaveData waveData)
    {
        Node startNode = GridManager.Instance.startNode;

        Vector3 spawnPos = new Vector3(startNode.x + 0.5f, startNode.y + 0.5f, 0);

        foreach (var monster in waveData.monsters)
        {
            for (int i = 0; i < monster.count; i++)
            {
                MonsterPoolManager.Instance.GetMonster(monster.type, spawnPos);

                yield return new WaitForSeconds(waveData.spawnInterval);
            }
        }
    }
}
