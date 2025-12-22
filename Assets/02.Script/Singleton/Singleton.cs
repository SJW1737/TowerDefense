using Unity.VisualScripting;
using UnityEngine;

public class Singleton<T> where T : new()
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }

    protected Singleton() { }
}

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    protected static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    var go = new GameObject(typeof(T).Name);
                    instance = go.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = (T)this;
        Init();
    }

    /// <summary>
    /// Awake 이후 초기화 지점
    /// </summary>
    protected virtual void Init() { }
}