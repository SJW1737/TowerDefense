using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    private Dictionary<GameObject, Queue<GameObject>> poolDict
        = new Dictionary<GameObject, Queue<GameObject>>();

    private Dictionary<GameObject, GameObject> instanceToPrefab
        = new Dictionary<GameObject, GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public GameObject Get(GameObject prefab)
    {
        if (!poolDict.ContainsKey(prefab))
        {
            poolDict[prefab] = new Queue<GameObject>();
        }

        if (poolDict[prefab].Count > 0)
        {
            GameObject obj = poolDict[prefab].Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject newObj = Instantiate(prefab);
            instanceToPrefab[newObj] = prefab;
            return newObj;
        }
    }

    public void ReturnToPool(MonoBehaviour component)
    {
        GameObject obj = component.gameObject;

        if (!instanceToPrefab.ContainsKey(obj))
        {
            Debug.LogWarning("이 오브젝트는 풀에 등록되지 않았습니다.");
            Destroy(obj);
            return;
        }

        GameObject prefab = instanceToPrefab[obj];

        obj.SetActive(false);
        poolDict[prefab].Enqueue(obj);
    }
}