using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniBossButtonUI : MonoBehaviour
{
    [SerializeField] private MiniBossData miniBossData;
    [SerializeField] private Button button;

    private void Awake()
    {
        if (button == null)
            button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        WaveManager.Instance.OnWaveChanged += OnWaveChanged;
        Refresh();
    }

    private void OnDisable()
    {
        if (WaveManager.Instance != null)
            WaveManager.Instance.OnWaveChanged -= OnWaveChanged;
    }

    void OnWaveChanged(int wave)
    {
        Refresh();
    }

    void Refresh()
    {
        var controller = MiniBossSpawnController.Instance;

        if (!controller.IsUnlocked(miniBossData))
        {
            button.interactable = false;
            return;
        }

        if (MonsterPoolManager.Instance.HasAliveMiniBoss())
        {
            button.interactable = false;
            return;
        }

        if (!controller.IsCooldownReady(miniBossData))
        {
            button.interactable = false;
            return;
        }

        button.interactable = true;
    }

    public void OnClickSpawn()
    {
        MiniBossSpawnController.Instance.Spawn(miniBossData);
        Refresh();
    }
}
