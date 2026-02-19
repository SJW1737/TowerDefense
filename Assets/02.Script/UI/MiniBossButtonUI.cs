using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniBossButtonUI : MonoBehaviour
{
    [SerializeField] private MiniBossData miniBossData;

    [SerializeField] private Button button;

    [SerializeField] private TextMeshProUGUI cooldownText;

    [SerializeField] private GameObject miniBossListPanel;
    [SerializeField] private GameObject dim;

    private Coroutine cooldownRoutine;

    private void Awake()
    {
        if (button == null)
            button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        WaveManager.Instance.OnWaveChanged += OnWaveChanged;
        RefreshImmediate();
    }

    private void OnDisable()
    {
        if (WaveManager.Instance != null)
            WaveManager.Instance.OnWaveChanged -= OnWaveChanged;

        StopCooldownRoutine();
    }

    void OnWaveChanged(int wave)
    {
        RefreshImmediate();
    }

    void RefreshImmediate()
    {
        StopCooldownRoutine();

        var controller = MiniBossSpawnController.Instance;

        if (!controller.IsUnlocked(miniBossData))
        {
            SetText("잠김");
            button.interactable = false;
            return;
        }

        if (MonsterPoolManager.Instance.HasAliveMiniBoss())
        {
            SetText("소환 중");
            button.interactable = false;
            return;
        }

        if (!controller.IsCooldownReady(miniBossData))
        {
            button.interactable = false;
            cooldownRoutine = StartCoroutine(CooldownRoutine());
            return;
        }

        SetText("소환");
        button.interactable = true;
    }

    public void OnClickSpawn()
    {
        bool success = MiniBossSpawnController.Instance.Spawn(miniBossData);

        if (success)
        {
            SoundManager.Instance.PlaySFX("ButtonClick");
            SoundManager.Instance.PlaySFX("BossAppear");

            RefreshImmediate();

            if (miniBossListPanel != null)
            {
                miniBossListPanel.SetActive(false);
            }

            if (dim != null)
            {
                dim.SetActive(false);
            }
        }
    }

    private  IEnumerator CooldownRoutine()
    {
        var controller = MiniBossSpawnController.Instance;

        while (!controller.IsCooldownReady(miniBossData))
        {
            if (MonsterPoolManager.Instance.HasAliveMiniBoss())
            {
                SetText("소환 중");
                yield break;
            }

            float remain = controller.GetRemainCooldown(miniBossData);
            SetText(FormatTime(remain));

            yield return new WaitForSeconds(0.2f);
        }

        //쿨타임 종료
        SetText("소환");
        button.interactable = true;
    }

    void StopCooldownRoutine()
    {
        if (cooldownRoutine != null)
        {
            StopCoroutine(cooldownRoutine);
            cooldownRoutine = null;
        }
    }

    void SetText(string text)
    {
        if (cooldownText)
            cooldownText.text = text;
    }

    string FormatTime(float time)
    {
        int sec = Mathf.CeilToInt(time);
        int minute = sec / 60;
        int second = sec % 60;
        return $"{minute}:{second:D2}";
    }
}
