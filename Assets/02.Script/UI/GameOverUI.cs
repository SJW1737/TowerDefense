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

        if (scoreAndBestText != null)
        {
            scoreAndBestText.text = $"Score : {waveScore} Wave\n" + $"Best : {bestWave} Wave";
        }

        Time.timeScale = 0f;
    }

    private void Restart()
    {
        Time.timeScale = 1f;

        var data = SaveManager.Instance.Data;
        data.startGoldApplied = false;
        SaveManager.Instance.Save();

        if (WaveManager.Instance != null) WaveManager.Instance.ResetWave();
        SceneManager.LoadScene(gameScene);
    }

    private void BackToTitle()
    {
        Time.timeScale = 1f;
        if (WaveManager.Instance != null) WaveManager.Instance.ResetWave();
        SceneManager.LoadScene(titleScene);
    }
}
