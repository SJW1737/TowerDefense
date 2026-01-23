using System.Collections;
using UnityEngine;

public class IceArea : MonoBehaviour
{
    private float radius;
    private float areaDuration;

    public void Init(float radius, float areaDuration)
    {
        this.radius = radius;
        this.areaDuration = areaDuration;

        transform.localScale = Vector3.one * radius * 2f;

        StartCoroutine(LifeRoutine());
        StartCoroutine(SlowRoutine());
    }

    private IEnumerator LifeRoutine()
    {
        yield return new WaitForSeconds(areaDuration);
        Destroy(gameObject);
    }

    private IEnumerator SlowRoutine()
    {
        WaitForSeconds tick = new WaitForSeconds(0.1f);

        while (true)
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
}
