using System.Collections.Generic;
using UnityEngine;

public class BombArea : MonoBehaviour
{
    public float duration;

    private float radius;
    private List<ITowerEffect> effects;

    public void Init(float radius, List<ITowerEffect> effects)
    {
        this.radius = radius;
        this.effects = effects;

        transform.localScale = Vector3.one * radius * 2f;

        ApplyDamage();
        Destroy(gameObject, duration);
    }

    private void ApplyDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, LayerMask.GetMask("Monster"));

        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent(out Monster monster))
                continue;

            foreach (var effect in effects)
                effect.Apply(monster);
        }
    }
}
