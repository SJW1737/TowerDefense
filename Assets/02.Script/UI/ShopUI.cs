using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private string titleSceneName = "TitleScene";
    [SerializeField] private Button backButton;

    private void Awake()
    {
        if (backButton != null)
            backButton.onClick.AddListener(OnClickBack);
    }

    private void OnClickBack()
    {
        SceneManager.LoadScene(titleSceneName);
    }
}
