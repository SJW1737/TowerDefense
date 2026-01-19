using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterHealth : Health
{
    [SerializeField] private MonsterData monsterData;
    [SerializeField] private MonsterHealthBarUI MonsterHelathBarUI;

    private Monster monster;

    private void Awake()
    {
        monster = GetComponent<Monster>();
    }

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
        monster.NotifyDead();
    }

    public void ResetHealth(int maxHp)
    {
        Init(maxHp);
        MonsterHelathBarUI.UpdateUI();
    }
}
