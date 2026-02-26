using System.Collections;
using UnityEngine;

public class IceArea : MonoBehaviour
{
    private float radius;
    private float areaDuration;

    private bool isInitialized;

    private Coroutine slowRoutine;

    public void Init(float radius, float areaDuration)
    {
        this.radius = radius;
        this.areaDuration = areaDuration;

        transform.localScale = Vector3.one * radius * 2f;

        isInitialized = true;

        StartCoroutine(LifeRoutine());
        slowRoutine = StartCoroutine(SlowRoutine());
    }

    private IEnumerator LifeRoutine()
    {
        yield return new WaitForSeconds(areaDuration);
        ReturnToPool();
    }

    private IEnumerator SlowRoutine()
    {
        WaitForSeconds tick = new WaitForSeconds(0.1f);

        while (isInitialized)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, LayerMask.GetMask("Monster"));

            foreach (var hit in hits)
            {
                if (!hit.TryGetComponent(out Monster monster))
                    continue;

                monster.ApplySlow(0.5f, 0.2f);
            }

            yield return tick;
        }
    }

    private void ReturnToPool()
    {
        isInitialized = false; 
        ObjectPool.Instance.ReturnToPool(this);
    }

    private void OnDisable()
    {
        isInitialized = false;

        StopAllCoroutines();

        radius = 0f;
        areaDuration = 0f;
    }
}
