using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public GameObject monsterPrefab;
    public int spawnCount = 5;              //스폰 수
    public float spawnInterval = 1f;      //스폰 간격

    private void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    IEnumerator SpawnMonsters()
    {
        Node startNode = GridManager.Instance.startNode;

        Vector3 spawnPos = new Vector3(startNode.x + 0.5f, startNode.y + 0.5f, 0);

        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(monsterPrefab, spawnPos, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
