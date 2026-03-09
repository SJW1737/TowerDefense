using System.Collections;
using UnityEngine;

public class MonsterBurn : MonoBehaviour
{
    private Coroutine burnRoutine;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private Color burnColor = new Color(0.7f, 0.1f, 0.1f, 1f);


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    private void OnEnable()
    {
        ResetColor();
    }

    public void ApplyBurn(float damagePerTick, float duration, float interval)
    {
        if (!gameObject.activeInHierarchy) 
            return;

        if (TryGetComponent(out Monster monster))
        {
            if (monster.IsDead) return;
        }

        // 이미 Burn 중이면 Coroutine 종료 후 새로 시작
        if (burnRoutine != null)
        {
            StopCoroutine(burnRoutine);
        }

        burnRoutine = StartCoroutine(BurnRoutine(damagePerTick, duration, interval));
    }

    private IEnumerator BurnRoutine(float damagePerTick, float duration, float interval)
    {
        float elapsed = 0f;

        // Burn 시작 -> 불꽃 켜기
        if (spriteRenderer != null)
            spriteRenderer.color = burnColor;

        while (elapsed < duration)
        {
            if (TryGetComponent(out Monster monster))
            {
                if (monster.IsDead)
                {
                    burnRoutine = null;
                    yield break;
                }

                monster.TakeDamage(damagePerTick);
            }

            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }

        ResetColor();

        burnRoutine = null;
    }

    //화상 강제 종료 함수
    public void StopBurn()
    {
        if (burnRoutine != null)
        {
            StopCoroutine(burnRoutine);
            burnRoutine = null;
        }

        ResetColor();
    }

    //화상 색상 초기화 함수
    public void ResetColor()
    {
        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;
    }

    private void OnDisable()
    {
        StopBurn();
    }
}
