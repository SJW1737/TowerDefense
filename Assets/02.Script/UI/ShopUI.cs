using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private string titleSceneName = "TitleScene";
    [SerializeField] private Button backButton;
    [SerializeField] private Button gachaButton;

    private void Awake()
    {
        if (backButton != null)
            backButton.onClick.AddListener(OnClickBack);

        if (gachaButton != null)
            gachaButton.onClick.AddListener(OnClickGacha);
    }

    private void OnClickBack()
    {
        SoundManager.Instance.PlaySFX("ButtonClick");
        SceneManager.LoadScene(titleSceneName);
    }

    private void OnClickGacha()
    {
        SoundManager.Instance.PlaySFX("ButtonClick");
        bool success = RelicGachaManager.Instance.TryGacha();
    }
}
