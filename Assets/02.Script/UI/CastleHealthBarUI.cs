using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private TextMeshProUGUI hpText;

    private CastleHealth castleHealth;

    public void Bind(CastleHealth health)
    {
        castleHealth = health;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (castleHealth == null || castleHealth.MaxHp <= 0) return;

        float ratio = (float)castleHealth.CurrentHp / castleHealth.MaxHp;
        hpSlider.value = ratio;
        hpText.text = $"{castleHealth.CurrentHp} / {castleHealth.MaxHp}";
    }
}
