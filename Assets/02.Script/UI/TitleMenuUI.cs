using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TitleMenuUI : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] private string gameSceneName = "GameScene";
    [SerializeField] private string shopSceneName = "ShopScene";

    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button settingButton;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI bestWaveText;

    [Header("UI")]
    [SerializeField] private SettingPanelUI settingPanelUI;

    private const string KEY_BEST_WAVE = "BEST_WAVE";

    private void Start()
    {
        UpdateBestWave();
    }

    private void UpdateBestWave()
    {
        int bestWave = PlayerPrefs.GetInt(KEY_BEST_WAVE, 0);
        if (bestWaveText != null)
            bestWaveText.text = $"Best Wave : {bestWave}";
    }

    public void OnClickStart()
    {
        if (startButton != null)
            startButton.interactable = false;

        var data = SaveManager.Instance.Data;
        data.startGoldApplied = false;
        SaveManager.Instance.Save();

        SceneManager.LoadScene(gameSceneName);
    }

    public void OnClickShop()
    {
        if (shopButton != null)
            shopButton.interactable = false;

        SceneManager.LoadScene(shopSceneName);
    }

    public void OnClickSetting()
    {
        if (settingPanelUI != null)
            settingPanelUI.Open();
    }
}
