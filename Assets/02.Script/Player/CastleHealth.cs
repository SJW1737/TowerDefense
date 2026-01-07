using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleHealth : Health
{
    [SerializeField] private int castleMaxHP = 20;
    [SerializeField] private CastleHealthBarUI CastleHealthBarUI;

    private bool isGameOver;


    private void Awake()
    {
        Init(castleMaxHP);
        CastleHealthBarUI.Bind(this);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        CastleHealthBarUI.UpdateUI();
    }

    protected override void Die()
    {
        Debug.Log("GameOver");

        GameOverUI ui = FindObjectOfType<GameOverUI>(true);
        if (ui == null)
        {
            return;
        }

        ui.Show(WaveManager.Instance.CurrentWave);
    }
}

