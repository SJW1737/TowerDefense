using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;

    private MonsterHealth monsterHealth;

    public void Bind(MonsterHealth health)
    {
        monsterHealth = health;

        hpSlider.minValue = 0f;
        hpSlider.maxValue = 1f;

        UpdateUI();
    }

    public void UpdateUI()
    {
        if (monsterHealth == null) return;

        float ratio = (float)monsterHealth.CurrentHp / monsterHealth.MaxHp;
        hpSlider.value = ratio;
    }
}
