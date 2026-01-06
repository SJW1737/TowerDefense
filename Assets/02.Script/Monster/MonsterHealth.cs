using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : Health
{
    [SerializeField] private MonsterData monsterData;
    [SerializeField] private MonsterHealthBarUI MonsterHelathBarUI;

    private void Start()
    {
        MonsterHelathBarUI.Bind(this);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        MonsterHelathBarUI.UpdateUI();
    }

    protected override void Die()
    {
        GetComponent<Monster>().OnDie();
    }

    public void ResetHealth(int maxHp)
    {
        Init(maxHp);
        MonsterHelathBarUI.UpdateUI();
    }
}
