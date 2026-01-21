using System.Collections;
using UnityEngine;

public class MonsterBurn : MonoBehaviour
{
    private Coroutine burnRoutine;

    public void ApplyBurn(int damagePerTick, float duration, float interval)
    {
        if (!gameObject.activeInHierarchy) return;

        if (burnRoutine != null)
            StopCoroutine(burnRoutine);

        burnRoutine = StartCoroutine(BurnRoutine(damagePerTick, duration, interval));
    }

    private IEnumerator BurnRoutine(int damagePerTick, float duration, float interval)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            if (TryGetComponent(out Monster monster))
                monster.TakeDamage(damagePerTick);

            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }

        burnRoutine = null;
    }
}
