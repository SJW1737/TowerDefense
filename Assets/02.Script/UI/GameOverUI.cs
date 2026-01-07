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

    [Header("Texts (TMP)")]
    [SerializeField] private TextMeshProUGUI scoreAndBestText;

    [Header("Buttons")]
    [SerializeField] private Button restartButton;
    [SerializeField] private Button backButton;

    [Header("Scene Names")]
    [SerializeField] private string gameScene = "GameScene";
    [SerializeField] private string titleScene = "TitleScene";

    private bool isShown;

    private void Awake()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (restartButton != null) restartButton.onClick.AddListener(Restart);
        if (backButton != null) backButton.onClick.AddListener(BackToTitle);
    }

    public void Show(int waveScore)
    {
        if (isShown) return;
        isShown = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (scoreAndBestText != null)
        {
            scoreAndBestText.text = $"Score : {waveScore} Wave\n" + $"Best : ?? Wave";
        }

        Time.timeScale = 0f;
    }

    private void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameScene);
    }

    private void BackToTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(titleScene);
    }
}
