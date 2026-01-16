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

    private WaveManager waveManager;

    private void Awake()
    {
        Debug.Log("[WavePopupUI] Awake");

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0f;

        waveManager = WaveManager.Instance;
    }

    private void OnEnable()
    {
        Debug.Log("[WavePopupUI] OnEnable - 이벤트 구독");

        if (waveManager != null)
            waveManager.OnWaveStarted += ShowWave;
    }

    private void OnDisable()
    {
        Debug.Log("[WavePopupUI] OnDisable - 이벤트 해제");

        if (waveManager != null)
            waveManager.OnWaveStarted -= ShowWave;
    }

    private void ShowWave(int wave)
    {
        Debug.Log($"[WavePopupUI] ShowWave 호출됨: {wave}");

        if (wave % 10 == 0)
            Show("BOSS WAVE");
        else
            Show($"{wave} WAVE");
    }

    private void Show(string text)
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
