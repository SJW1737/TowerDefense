using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Monster target;
    protected float speed;
    protected List<ITowerEffect> effects;

    private bool isInitialized;

    public GameObject PrefabKey { get; private set; }

    public void SetPrefabKey(GameObject key)
    {
        PrefabKey = key;
    }

    public virtual void Init(Monster target, float speed, List<ITowerEffect> effects)
    {
        this.target = target;
        this.speed = speed;
        this.effects = effects != null ? new List<ITowerEffect>(effects) : null;

        if (target != null)
            target.OnDeath += OnTargetDeath;

        isInitialized = true;
    }

    protected virtual void Update()
    {
        if (!isInitialized)
            return;

        if (target == null || !target.gameObject.activeInHierarchy || target.IsDead)
        {
            ReturnToPool();
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!isInitialized)
            return;
        if (!other.TryGetComponent(out Monster monster))
            return;
        if (monster != target)
            return;
        if (monster.IsDead)
            return;

        if (effects != null)
        {
            foreach (var effect in effects)
                effect.Apply(monster);
        }

        ReturnToPool();
    }

    private void OnTargetDeath(Monster deadMonster)
    {
        ReturnToPool();
    }


    protected virtual void ReturnToPool()
    {
        isInitialized = false;
        ObjectPool.Instance.ReturnToPool(this);
    }

    private void OnDisable()
    {
        if (target != null)
            target.OnDeath -= OnTargetDeath;

        target = null;
        effects = null;
        speed = 0f;
        isInitialized = false;

        StopAllCoroutines();
    }
}
