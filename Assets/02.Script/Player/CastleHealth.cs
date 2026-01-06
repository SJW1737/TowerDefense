using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleHealth : Health
{
    [SerializeField] private int castleMaxHP = 20;
    [SerializeField] private CastleHealthBarUI CastleHealthBarUI;

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
    }
}
