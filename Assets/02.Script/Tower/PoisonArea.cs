using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArea : MonoBehaviour
{
    private float radius;
    private List<ITowerEffect> effects;

    public void Init(float radius, float duration, List<ITowerEffect> effects)
    {
        this.radius = radius;
        this.effects = effects;

        transform.localScale = Vector3.one * radius * 4f;

        StartCoroutine(DamageRoutine());
        Destroy(gameObject, duration);
    }

    private IEnumerator DamageRoutine()
    {
        WaitForSeconds tick = new WaitForSeconds(1f);

        while (true)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, LayerMask.GetMask("Monster"));

            foreach (var hit in hits)
            {
                if (!hit.TryGetComponent(out Monster monster))
                    continue;

                foreach (var effect in effects)
                    effect.Apply(monster);
            }

            yield return tick;
        }
    }
}
