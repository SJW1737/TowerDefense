using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveDelayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI delayText;

    private CanvasGroup canvasGroup;
    private Coroutine routine;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        canvasGroup.alpha = 0f;
    }

    public void StartCountdown(float time)
    {
        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(CountdownRoutine(time));
    }

    private IEnumerator CountdownRoutine(float time)
    {
        canvasGroup.alpha = 1f;

        while (time > 0)
        {
            delayText.text = $"시작까지 {Mathf.CeilToInt(time)}";
            yield return new WaitForSeconds(1f);
            time -= 1f;
        }

        canvasGroup.alpha = 0f;
    }
}
