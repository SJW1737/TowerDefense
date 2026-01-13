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
            SetText("LOCKED");
            button.interactable = false;
            return;
        }

        if (MonsterPoolManager.Instance.HasAliveMiniBoss())
        {
            SetText("ACTIVE");
            button.interactable = false;
            return;
        }

        if (!controller.IsCooldownReady(miniBossData))
        {
            button.interactable = false;
            cooldownRoutine = StartCoroutine(CooldownRoutine());
            return;
        }

        SetText("SPAWN");
        button.interactable = true;
    }

    public void OnClickSpawn()
    {
        MiniBossSpawnController.Instance.Spawn(miniBossData);
        RefreshImmediate();
    }

    IEnumerator CooldownRoutine()
    {
        var controller = MiniBossSpawnController.Instance;

        while (!controller.IsCooldownReady(miniBossData))
        {
            //중간에 미니보스가 생기면 중단
            if (MonsterPoolManager.Instance.HasAliveMiniBoss())
            {
                SetText("ACTIVE");
                yield break;
            }

            float remain = controller.GetRemainCooldown(miniBossData);
            SetText(FormatTime(remain));

            yield return new WaitForSeconds(0.2f);
        }

        //쿨타임 종료
        SetText("SPAWN");
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
