using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoolManager : MonoSingleton<MonsterPoolManager>
{
    public List<MonsterPoolData> poolDatas;

    private Dictionary<MonsterType, Queue<GameObject>> poolDict;
    private Dictionary<MonsterType, GameObject> prefabDict;

    public int AliveMonsterCount { get; private set; }

    protected override void Init()
    {
        poolDict = new Dictionary<MonsterType, Queue<GameObject>>();
        prefabDict = new Dictionary<MonsterType, GameObject>();

        foreach (var data in poolDatas)
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

        AliveMonsterCount = 0;
    }

    public GameObject GetMonster(MonsterType type, Vector3 position)
    {
        if (!poolDict.ContainsKey(type))
        {
            return null;
        }

        GameObject monster;

        if (poolDict[type].Count > 0)
        {
            monster = poolDict[type].Dequeue();
        }
        else
        {
            monster = Instantiate(prefabDict[type]);
        }

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
}
