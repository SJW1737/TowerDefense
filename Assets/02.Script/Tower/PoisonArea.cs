using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArea : MonoBehaviour
{
    private Monster owner;
    private float radius;
    private float remainTime;
    private List<ITowerEffect> effects;

    private bool isReturned;

    public void Init(Monster owner, float radius, float duration, List<ITowerEffect> effects)
    {
        this.owner = owner;
        this.radius = radius;
        this.effects = effects;

        remainTime = duration;
        transform.localScale = Vector3.one * radius * 2f;

        isReturned = false;

        StartCoroutine(DamageRoutine());
    }

    private void Update()
    {
        if (owner != null)
            transform.position = owner.transform.position;

        remainTime -= Time.deltaTime;

        if (remainTime <= 0f)
        {
            ReturnToPool();
        }
    }

    public void Refresh(float duration)
    {
        remainTime = duration;
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

    private void ReturnToPool()
    {
        if (isReturned)
            return;

        isReturned = true;

        StopAllCoroutines();
        owner?.ClearPoison();
        ObjectPool.Instance.ReturnToPool(this);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        owner = null;
        effects = null;
        isReturned = false;
    }
}
