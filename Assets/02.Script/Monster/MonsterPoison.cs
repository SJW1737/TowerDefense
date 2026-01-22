using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoison : MonoBehaviour
{
    [SerializeField] private GameObject poisonPrefab;

    private GameObject poisonInstance;
    private Coroutine poisonRoutine;

    private void Awake()
    {
        if (poisonPrefab != null)
        {
            poisonInstance = Instantiate(poisonPrefab, transform);
            poisonInstance.transform.localPosition = Vector3.zero;
            poisonInstance.SetActive(false);
        }
    }

    public void ApplyPoison(float damagePerTick, float duration, float interval)
    {
        if (!gameObject.activeInHierarchy)
            return;

        // 이미 Poison 중이면 Coroutine 종료 후 새로 시작
        if (poisonRoutine != null)
        {
            StopCoroutine(poisonRoutine);

            // 이전 독 끄기
            if (poisonInstance != null)
                poisonInstance.SetActive(false);
        }

        poisonRoutine = StartCoroutine(PoisonRoutine(damagePerTick, duration, interval));
    }

    private IEnumerator PoisonRoutine(float damagePerTick, float duration, float interval)
    {
        float elapsed = 0f;

        // Poison 시작 -> 독 켜기
        if (poisonInstance != null)
            poisonInstance.SetActive(true);

        while (elapsed < duration)
        {
            if (TryGetComponent(out Monster monster))
                monster.TakeDamage(damagePerTick);

            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }

        // Poison 종료 -> 독 끄기
        if (poisonInstance != null)
            poisonInstance.SetActive(false);

        poisonRoutine = null;
    }
}
