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
        SceneManager.LoadScene(titleSceneName);
    }

    private void OnClickGacha()
    {
        bool success = RelicGachaManager.Instance.TryGacha();
    }
}
