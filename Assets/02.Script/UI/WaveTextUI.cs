using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveText;

    private WaveManager waveManager;

    private void OnEnable()
    {
        waveManager = WaveManager.Instance;
        if (waveManager != null)
        {
            waveManager.OnWaveChanged += HandleWaveChanged;

            HandleWaveChanged(waveManager.CurrentWave);
        }
            
    }

    private void OnDisable()
    {
        if (waveManager != null)
            waveManager.OnWaveChanged -= HandleWaveChanged;

        waveManager = null;
    }

    private void HandleWaveChanged(int wave)
    {
        if (waveText != null)
            waveText.text = $"¿þÀÌºê : {wave}";
    }
}
