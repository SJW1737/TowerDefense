using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiamondUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI diamondText;

    private void Start()
    {
        Refresh(SaveManager.Instance.Diamond);
        SaveManager.Instance.OnDiamondChanged += Refresh;
    }

    private void OnDestroy()
    {
        if (SaveManager.Instance != null)
            SaveManager.Instance.OnDiamondChanged -= Refresh;
    }

    private void Refresh(int value)
    {
        diamondText.text = value.ToString();
    }
}
