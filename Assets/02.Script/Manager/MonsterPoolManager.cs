using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoolManager : MonoSingleton<MonsterPoolManager>
{
    public List<MonsterPoolData> poolDatas;

    private Dictionary<MonsterType, Queue<GameObject>> monsterPools;
    private Dictionary<MonsterType, MonsterData> monsterDatas;

    //추가 생성 용
    private Dictionary<MonsterType, GameObject> monsterPrefabs;

    private Dictionary<MiniBossData, Queue<GameObject>> miniBossPools;

    //추가 생성 용
    private Dictionary<MiniBossData, GameObject> miniBossPrefabs;

    public int AliveMonsterCount { get; private set; }
    public int AliveMiniBossCount { get; private set; }

    protected override void Init()
    {
        monsterPools = new Dictionary<MonsterType, Queue<GameObject>>();
        monsterDatas = new Dictionary<MonsterType, MonsterData>();
        monsterPrefabs = new Dictionary<MonsterType, GameObject>();
        miniBossPools = new Dictionary<MiniBossData, Queue<GameObject>>();
        miniBossPrefabs = new Dictionary<MiniBossData, GameObject>();

        foreach (var data in poolDatas)
        {
            //일반 몬스터용(웨이브)
            if (!monsterPools.ContainsKey(data.type))
            {
                Queue<GameObject> pool = new Queue<GameObject>();

                for (int i = 0; i < data.poolSize; i++)
                {
                    GameObject obj = Instantiate(data.prefab);
                    obj.SetActive(false);
                    pool.Enqueue(obj);
                }

                monsterPools.Add(data.type, pool);
                monsterDatas.Add(data.type, data.monsterData);

                monsterPrefabs.Add(data.type, data.prefab);
            }

            //미니보스용
            if (data.miniBossData != null && !miniBossPools.ContainsKey(data.miniBossData))
            {
                Queue<GameObject> pool = new Queue<GameObject>();

                for (int i = 0; i < data.poolSize; i++)
                {
                    GameObject obj = Instantiate(data.prefab);
                    obj.SetActive(false);
                    pool.Enqueue(obj);
                }

                miniBossPools.Add(data.miniBossData, pool);

                miniBossPrefabs.Add(data.miniBossData, data.prefab);
            }
        }

        AliveMonsterCount = 0;
        AliveMiniBossCount = 0;
    }

    public GameObject GetMonster(MonsterType type, Vector3 position)    //일반 몬스터용
    {
        if (!monsterPools.ContainsKey(type))
        {
            return null;
        }

        GameObject obj;

        if (monsterPools[type].Count == 0)
        {
            obj = Instantiate(monsterPrefabs[type]);    //부족하면 새로 생성
        }
        else
        {
            obj = monsterPools[type].Dequeue();
        }

        obj.transform.position = position;

        Monster monster = obj.GetComponent<Monster>();
        monster.Init(monsterDatas[type]);
        obj.SetActive(true);
        monster.Activate();

        AliveMonsterCount++;
        return obj;
    }

    public GameObject GetMiniBoss(MiniBossData data, Vector3 position) //미니보스용
    {
        if (!miniBossPools.ContainsKey(data))
        {
            return null;
        }

        GameObject obj;

        if (miniBossPools[data].Count == 0)
        {
            obj = Instantiate(miniBossPrefabs[data]);   //부족하면 새로 생성
        }
        else
        {
            obj = miniBossPools[data].Dequeue();
        }

        obj.transform.position = position;

        Monster monster = obj.GetComponent<Monster>();
        monster.Init(data.monsterData, data);
        obj.SetActive(true);
        monster.Activate();

        AliveMonsterCount++;
        AliveMiniBossCount++;
        return obj;
    }

    public void ReturnMonster(Monster monster)
    {
        monster.gameObject.SetActive(false);
        monsterPools[monster.MonsterData.monsterType].Enqueue(monster.gameObject);
        AliveMonsterCount--;
    }

    public void ReturnMiniBoss(Monster monster)
    {
        monster.gameObject.SetActive(false);
        miniBossPools[monster.MiniBossData].Enqueue(monster.gameObject);
        AliveMonsterCount--;
        AliveMiniBossCount--;
    }

    public bool HasAliveMiniBoss()
    {
        return AliveMiniBossCount > 0;
    }
}
