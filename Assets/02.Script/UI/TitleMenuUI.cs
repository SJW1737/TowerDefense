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
    [SerializeField] private Button exitButton;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI bestWaveText;

    [Header("UI")]
    [SerializeField] private SettingPanelUI settingPanelUI;
    [SerializeField] private DailyMissionPanelUI dailyMissionPanelUI;
    [SerializeField] private AchieveMentPanelUI achievementPanelUI;

    private const string KEY_BEST_WAVE = "BEST_WAVE";

    private void Start()
    {
        UpdateBestWave();
        SoundManager.Instance.PlayBGM("TitleBGM");
    }

    private void UpdateBestWave()
    {
        int bestWave = PlayerPrefs.GetInt(KEY_BEST_WAVE, 0);
        if (bestWaveText != null)
            bestWaveText.text = $"최고 웨이브 : {bestWave}";
    }

    public void OnClickStart()
    {
        if (startButton != null)
            startButton.interactable = false;

        var data = SaveManager.Instance.Data;
        data.startGoldApplied = false;
        SaveManager.Instance.Save();

        SoundManager.Instance.PlaySFX("ButtonClick");

        SceneManager.LoadScene(gameSceneName);
    }

    public void OnClickShop()
    {
        if (shopButton != null)
            shopButton.interactable = false;

        SceneManager.LoadScene(shopSceneName);
    }

    public void OnClickAchievement()
    {
        SoundManager.Instance.PlaySFX("ButtonClick");

        if (achievementPanelUI != null)
            achievementPanelUI.Open();
    }

    public void OnClickDailyMission()
    {
        SoundManager.Instance.PlaySFX("ButtonClick");

        if (dailyMissionPanelUI != null)
            dailyMissionPanelUI.Open();
    }

    public void OnClickSetting()
    {
        SoundManager.Instance.PlaySFX("ButtonClick");

        if (settingPanelUI != null)
            settingPanelUI.Open();
    }

    public void OnClickExit()
    {
        SoundManager.Instance.PlaySFX("ButtonClick");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //에디터
#else
        Application.Quit(); //빌드
#endif
    }
}
