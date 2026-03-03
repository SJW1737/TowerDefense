using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Monster target;
    protected float speed;
    protected List<ITowerEffect> effects;

    protected bool isInitialized;

    public virtual void Init(Monster target, float speed, List<ITowerEffect> effects)
    {
        this.target = target;
        this.speed = speed;
        this.effects = effects != null ? new List<ITowerEffect>(effects) : null;

        isInitialized = true;
    }

    protected virtual void Update()
    {
        if (!isInitialized)
            return;

        if (target == null || target.IsDead || !target.gameObject.activeInHierarchy)
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

    protected virtual void ReturnToPool()
    {
        if (target != null)
        {
            target = null;
        }

        isInitialized = false;
        effects = null;
        speed = 0f;

        ObjectPool.Instance.ReturnToPool(this);
    }
}
