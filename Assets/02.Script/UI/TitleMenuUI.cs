using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenuUI : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] private string gameSceneName = "GameScene";

    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingButton;

    [Header("UI")]
    [SerializeField] private SettingPanelUI settingPanelUI;


    public void OnClickStart()
    {
        if (startButton != null)
            startButton.interactable = false;

        SceneManager.LoadScene(gameSceneName);
    }

    public void OnClickSetting()
    {
        if (settingPanelUI != null)
            settingPanelUI.Open();
    }
}
