using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoolManager : MonoSingleton<MonsterPoolManager>
{
    public GameObject monsterPrefab;
    public int poolSize = 20;

    private Queue<GameObject> pool = new Queue<GameObject>();

    protected override void Init()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject monster = Instantiate(monsterPrefab);
            monster.SetActive(false);
            pool.Enqueue(monster);
        }
    }

    public GameObject GetMonster(Vector3 position)
    {
        GameObject monster;

        if (pool.Count > 0)
        {
            monster = pool.Dequeue();
        }
        else
        {
            monster = Instantiate(monsterPrefab);
        }

        monster.transform.position = position;
        monster.SetActive(true);

        monster.GetComponent<Monster>().Activate();

        return monster;
    }

    public void ReturnMonster(GameObject monster)
    {
        monster.SetActive(false);
        pool.Enqueue(monster);
    }
}
