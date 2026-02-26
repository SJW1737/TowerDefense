using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombArea : MonoBehaviour
{
    public float duration;

    private float radius;
    private List<ITowerEffect> effects;

    private bool isReturned;
    private int monsterLayer;

    private void Awake()
    {
        monsterLayer = LayerMask.GetMask("Monster");
    }

    public void Init(float radius, List<ITowerEffect> effects)
    {
        this.radius = radius;
        this.effects = effects;

        transform.localScale = Vector3.one * radius * 2f;

        isReturned = false;

        ApplyDamage();
        StartCoroutine(ReturnAfterTime());
    }

    private IEnumerator ReturnAfterTime()
    {
        yield return new WaitForSeconds(duration);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (isReturned)
            return;

        isReturned = true;

        StopAllCoroutines();
        ObjectPool.Instance.ReturnToPool(this);
    }

    private void ApplyDamage()
    {
        if (effects == null)
            return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, LayerMask.GetMask("Monster"));

        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent(out Monster monster))
                continue;

            foreach (var effect in effects)
                effect.Apply(monster);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        effects = null;
        isReturned = false;
    }
}
