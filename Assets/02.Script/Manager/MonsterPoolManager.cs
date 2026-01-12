using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoolManager : MonoSingleton<MonsterPoolManager>
{
    public List<MonsterPoolData> poolDatas;

    private Dictionary<MonsterType, Queue<GameObject>> poolDict;
    private Dictionary<MonsterType, GameObject> prefabDict;

    private Dictionary<MonsterData, Queue<GameObject>> poolByData;
    private Dictionary<MonsterData, GameObject> prefabByData;

    public int AliveMonsterCount { get; private set; }
    public int AliveMiniBossCount { get; private set; }

    protected override void Init()
    {
        poolDict = new Dictionary<MonsterType, Queue<GameObject>>();
        prefabDict = new Dictionary<MonsterType, GameObject>();

        poolByData = new Dictionary<MonsterData, Queue<GameObject>>();
        prefabByData = new Dictionary<MonsterData, GameObject>();

        foreach (var data in poolDatas)
        {
            //일반 몬스터용(웨이브)
            if (!poolDict.ContainsKey(data.type))
            {
                Queue<GameObject> pool = new Queue<GameObject>();

                for (int i = 0; i < data.poolSize; i++)
                {
                    GameObject monster = Instantiate(data.prefab);
                    monster.SetActive(false);
                    pool.Enqueue(monster);
                }

                poolDict.Add(data.type, pool);
                prefabDict.Add(data.type, data.prefab);
            }

            //미니보스용
            if (data.monsterData != null)
            {
                Queue<GameObject> dataPool = new Queue<GameObject>();

                for (int i = 0; i < data.poolSize; i++)
                {
                    GameObject monster = Instantiate(data.prefab);
                    monster.SetActive(false);
                    dataPool.Enqueue(monster);
                }

                poolByData.Add(data.monsterData, dataPool);
                prefabByData.Add(data.monsterData, data.prefab);
            }
        }

        AliveMonsterCount = 0;
        AliveMiniBossCount = 0;
    }

    public GameObject GetMonster(MonsterType type, Vector3 position)    //일반 몬스터용
    {
        if (!poolDict.ContainsKey(type))
        {
            return null;
        }

        GameObject monster = poolDict[type].Count > 0 ? poolDict[type].Dequeue() : Instantiate(prefabDict[type]);

        monster.transform.position = position;
        monster.SetActive(true);

        monster.GetComponent<Monster>().Activate();

        AliveMonsterCount++;
        return monster;
    }

    public void ReturnMonster(MonsterType type, GameObject monster)
    {
        monster.SetActive(false);
        poolDict[type].Enqueue(monster);

        AliveMonsterCount--;
    }

    public GameObject GetMonster(MonsterData monsterData, Vector3 position) //미니보스용
    {
        if (!poolByData.ContainsKey(monsterData))
        {
            return null;
        }

        GameObject monster = poolByData[monsterData].Count > 0 ? poolByData[monsterData].Dequeue() : Instantiate(prefabByData[monsterData]);

        monster.transform.position = position;
        monster.SetActive(true);
        monster.GetComponent<Monster>().Activate();

        AliveMonsterCount++;

        if (monsterData.monsterType == MonsterType.MiniBoss)
            AliveMiniBossCount++;

        return monster;
    }

    public void ReturnMonster(MonsterData monsterData, GameObject monster)
    {
        monster.SetActive(false);
        poolByData[monsterData].Enqueue(monster);
        AliveMonsterCount--;

        if (monsterData.monsterType == MonsterType.MiniBoss)
            AliveMiniBossCount--;
    }

    public bool HasMonsterDataPool(MonsterData data)
    {
        return poolByData.ContainsKey(data);
    }

    public bool HasAliveMiniBoss()
    {
        return AliveMiniBossCount > 0;
    }
}
