using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject dim;

    [SerializeField] private Button backButton;

    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private void Awake()
    {
        if (settingPanel != null) settingPanel.SetActive(false);
        if (dim != null) dim.SetActive(false);

        if (backButton != null)
            backButton.onClick.AddListener(Close);
    }

    public void Open()
    {
        if (dim != null) dim.SetActive(true);
        if (settingPanel != null) settingPanel.SetActive(true);
    }

    public void Close()
    {
        if (settingPanel != null) settingPanel.SetActive(false);
        if (dim != null) dim.SetActive(false);
    }
}
