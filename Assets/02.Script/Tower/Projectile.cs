using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Monster target;
    private float speed;
    private List<ITowerEffect> effects;

    public void Init(Monster target, float speed, List<ITowerEffect> effects)
    {
        this.target = target;
        this.speed = speed;
        this.effects = effects;
    }

    private void Update()
    {
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 dir = (target.transform.position - transform.position).normalized;
        transform.position += (Vector3)(dir * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Monster monster))
            return;

        foreach (var effect in effects)
            effect.Apply(monster);

        Destroy(gameObject);
    }
}
