using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WavePopupUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI popupText;
    [SerializeField] private float showDuration = 1.5f;

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

    public void Show(string text)
    {
        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(ShowRoutine(text));
    }

    private IEnumerator ShowRoutine(string text)
    {
        popupText.text = text;
        canvasGroup.alpha = 1f;

        yield return new WaitForSeconds(showDuration);

        canvasGroup.alpha = 0f;
    }
}
