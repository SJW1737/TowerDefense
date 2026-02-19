using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject gameOverPanel;

    [Header("Dim")]
    [SerializeField] private GameObject dim;

    [Header("Texts (TMP)")]
    [SerializeField] private TextMeshProUGUI scoreAndBestText;

    [Header("Buttons")]
    [SerializeField] private Button restartButton;
    [SerializeField] private Button backButton;

    [Header("Scene Names")]
    [SerializeField] private string gameScene = "GameScene";
    [SerializeField] private string titleScene = "TitleScene";

    private bool isShown;

    private const string KEY_BEST_WAVE = "BEST_WAVE";

    private void Awake()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (dim != null)
            dim.SetActive(false);

        if (restartButton != null) restartButton.onClick.AddListener(Restart);
        if (backButton != null) backButton.onClick.AddListener(BackToTitle);
    }

    public void Show(int waveScore)
    {
        if (isShown) return;
        isShown = true;

        if (dim != null)
            dim.SetActive(true);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        int bestWave = PlayerPrefs.GetInt(KEY_BEST_WAVE, 0);
        if (waveScore > bestWave)
        {
            bestWave = waveScore;
            PlayerPrefs.SetInt(KEY_BEST_WAVE, bestWave);
            PlayerPrefs.Save();
        }

        int rewardDiamond = 0;
        if (WaveManager.Instance != null)
        {
            rewardDiamond = WaveManager.Instance.GetWaveClearDiamondReward();
        }

        if (scoreAndBestText != null)
        {
            scoreAndBestText.text = $"웨이브 : {waveScore} 웨이브\n" + $"최고 웨이브 : {bestWave} 웨이브\n\n" + $"보상 : \n +{rewardDiamond} 다이아몬드";
        }

        Time.timeScale = 0f;
    }

    private void Restart()
    {
        SoundManager.Instance.StopBGM();

        Time.timeScale = 1f;

        var data = SaveManager.Instance.Data;
        data.startGoldApplied = false;
        SaveManager.Instance.Save();

        if (WaveManager.Instance != null) WaveManager.Instance.ResetWave();
        SceneManager.LoadScene(gameScene);
    }

    private void BackToTitle()
    {
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlayBGM("TitleBGM");

        Time.timeScale = 1f;
        if (WaveManager.Instance != null) WaveManager.Instance.ResetWave();
        SceneManager.LoadScene(titleScene);
    }
}
