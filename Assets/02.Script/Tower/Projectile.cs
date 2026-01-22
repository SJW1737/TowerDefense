using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Monster target;
    protected float speed;
    protected List<ITowerEffect> effects;

    public virtual void Init(Monster target, float speed, List<ITowerEffect> effects)
    {
        this.target = target;
        this.speed = speed;
        this.effects = effects;
    }

    protected virtual void Update()
    {
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Monster monster))
            return;
        if (monster != target)
            return;

        foreach (var effect in effects)
            effect.Apply(monster);

        Destroy(gameObject);
    }
}
