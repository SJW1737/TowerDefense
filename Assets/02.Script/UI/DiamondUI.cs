using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiamondUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI diamondText;

    private const string FORMAT = " : {0:N0}";

    private SaveManager saveManager;

    private void OnEnable()
    {
        saveManager = SaveManager.Instance;

        Refresh(saveManager.Diamond);
        saveManager.OnDiamondChanged += Refresh;
    }

    private void OnDisable()
    {
        if (saveManager != null)
            saveManager.OnDiamondChanged -= Refresh;
    }

    private void Refresh(int value)
    {
        diamondText.text = string.Format(FORMAT, value);
    }
}
