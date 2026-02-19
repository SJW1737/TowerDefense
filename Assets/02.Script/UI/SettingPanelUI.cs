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

        // 슬라이더 이벤트 연결
        if (bgmSlider != null)
            bgmSlider.onValueChanged.AddListener(OnBGMChanged);

        if (sfxSlider != null)
            sfxSlider.onValueChanged.AddListener(OnSFXChanged);
    }

    public void Open()
    {
        if (dim != null) dim.SetActive(true);
        if (settingPanel != null) settingPanel.SetActive(true);

        // 열릴 때 현재 볼륨 값으로 초기화
        bgmSlider.value = SoundManager.Instance.bgmVolume;
        sfxSlider.value = SoundManager.Instance.sfxVolume;
    }

    public void Close()
    {
        SoundManager.Instance.PlaySFX("ButtonClick");

        if (settingPanel != null) settingPanel.SetActive(false);
        if (dim != null) dim.SetActive(false);
    }

    private void OnBGMChanged(float value)
    {
        SoundManager.Instance.SetBGMVolume(value);
    }

    private void OnSFXChanged(float value)
    {
        SoundManager.Instance.SetSFXVolume(value);
    }
}
