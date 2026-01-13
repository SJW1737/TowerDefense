using System.Collections;
using UnityEngine;

public class MonsterStatusEffect : MonoBehaviour
{
    private MonsterHealth health;

    private void Awake()
    {
        health = GetComponent<MonsterHealth>();
    }

    public void ApplyDot(float damagePerTick, float duration, float interval)
    {
        StartCoroutine(DotRoutine(damagePerTick, duration, interval));
    }

    private IEnumerator DotRoutine(float damagePerTick, float duration, float interval)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            health.TakeDamage(damagePerTick);
            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }
    }
}
