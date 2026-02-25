using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WavePopupUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI popupText;
    [SerializeField] private float normalDuration = 1f;
    [SerializeField] private float bossDuration = 0.2f;

    private CanvasGroup canvasGroup;
    private Coroutine routine;

    private bool isPreparing;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0f;
    }

    private void OnEnable()
    {
        if (WaveManager.Instance != null)
            WaveManager.Instance.OnPrepareTimeChanged += UpdatePrepareTime;
    }

    private void OnDisable()
    {
        if (WaveManager.Instance != null)
            WaveManager.Instance.OnPrepareTimeChanged -= UpdatePrepareTime;
    }

    public IEnumerator ShowWave(string text, bool isBoss = false)
    {
        if (routine != null)
            StopCoroutine(routine);

        isPreparing = false;

        popupText.text = text;
        canvasGroup.alpha = 1f;

        float duration = isBoss ? bossDuration : normalDuration;
        yield return new WaitForSeconds(duration);

        canvasGroup.alpha = 0f;
    }

    public void Show(string text, bool isBoss = false)
    {
        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(ShowRoutine(text, isBoss));
    }

    private IEnumerator ShowRoutine(string text, bool isBoss)
    {
        isPreparing = false;

        popupText.text = text;
        canvasGroup.alpha = 1f;

        float duration = isBoss ? bossDuration : normalDuration;
        yield return new WaitForSeconds(duration);

        canvasGroup.alpha = 0f;
    }

    private void UpdatePrepareTime(float time)
    {
        if (time > 0f)
        {
            isPreparing = true;
            canvasGroup.alpha = 1f;
            popupText.text = $"다음 웨이브까지 {Mathf.CeilToInt(time)}";
        }
        else if (isPreparing)
        {
            canvasGroup.alpha = 0f;
            isPreparing = false;
        }
    }

    public void Hide()
    {
        canvasGroup.alpha = 0f;
        isPreparing = false;
    }
}
