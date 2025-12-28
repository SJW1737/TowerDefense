using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleHealth : Health
{
    [SerializeField] private int castleMaxHP = 100;
    [SerializeField] private CastleHealthBarUI healthBarUI;

    private void Start()
    {
        Init(castleMaxHP);
        healthBarUI.Bind(this);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        healthBarUI.UpdateUI();
    }

    protected override void Die()
    {
        Debug.Log("GameOver");
    }
}
