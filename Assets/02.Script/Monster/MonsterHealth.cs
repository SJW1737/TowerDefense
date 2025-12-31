using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : Health
{
    [SerializeField] private MonsterData monsterData;
    [SerializeField] private MonsterHealthBarUI MonsterHelathBarUI;

    private void Start()
    {
        Init(monsterData.maxHP);
        MonsterHelathBarUI.Bind(this);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        MonsterHelathBarUI.UpdateUI();
    }

    protected override void Die()
    {
        GetComponent<Monster>().OnDie();
    }

    public void ResetHealth()
    {
        Init(monsterData.maxHP);
        MonsterHelathBarUI.UpdateUI();
    }
}
