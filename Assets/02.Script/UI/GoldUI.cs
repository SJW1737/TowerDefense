using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;

    private void Start()
    {
        GoldManager.Instance.OnGoldChanged.AddListener(UpdateGold);
        UpdateGold(GoldManager.Instance.CurrentGold);
    }

    private void UpdateGold(int gold)
    {
        goldText.text = gold.ToString();
    }
}
