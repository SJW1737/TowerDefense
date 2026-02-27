using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WavePopupUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI popupText;
    [SerializeField] private float normalDuration = 1f;
    [SerializeField] private float bossDuration = 0.8f;

    private CanvasGroup canvasGroup;
    private Coroutine routine;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

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
        popupText.text = text;
        canvasGroup.alpha = 1f;

        float duration = isBoss ? bossDuration : normalDuration;
        yield return new WaitForSeconds(duration);

        canvasGroup.alpha = 0f;
    }
}
