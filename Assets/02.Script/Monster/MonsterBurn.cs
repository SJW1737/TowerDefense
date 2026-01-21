using System.Collections;
using UnityEngine;

public class MonsterBurn : MonoBehaviour
{
    private Coroutine burnRoutine;
    public GameObject burnPrefab;
    private GameObject burnInstance;

    private void Awake()
    {
        // 몬스터가 소환될 때 burnPrefab 자동 생성
        if (burnPrefab != null)
        {
            burnInstance = Instantiate(burnPrefab, transform);
            burnInstance.transform.localPosition = Vector3.zero;
            burnInstance.SetActive(false);
        }
    }

    public void ApplyBurn(int damagePerTick, float duration, float interval)
    {
        if (!gameObject.activeInHierarchy) return;


        // 이미 Burn 중이면 Coroutine 종료 후 새로 시작
        if (burnRoutine != null)
        {
            StopCoroutine(burnRoutine);

            // 이전 불꽃 끄기
            if (burnInstance != null)
                burnInstance.SetActive(false);
        }

        burnRoutine = StartCoroutine(BurnRoutine(damagePerTick, duration, interval));
    }

    private IEnumerator BurnRoutine(int damagePerTick, float duration, float interval)
    {
        float elapsed = 0f;

        // Burn 시작 -> 불꽃 켜기
        if (burnInstance != null)
            burnInstance.SetActive(true);

        while (elapsed < duration)
        {
            if (TryGetComponent(out Monster monster))
                monster.TakeDamage(damagePerTick);

            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }

        // Burn 종료 -> 불꽃 끄기
        if (burnInstance != null)
            burnInstance.SetActive(false);
        
        burnRoutine = null;
    }
}
