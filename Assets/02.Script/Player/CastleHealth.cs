using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleHealth : Health
{
    [SerializeField] private int castleMaxHP = 100;

    private void Start()
    {
        Init(castleMaxHP);
    }

    protected override void Die()
    {
        Debug.Log("GameOver");
    }
}
