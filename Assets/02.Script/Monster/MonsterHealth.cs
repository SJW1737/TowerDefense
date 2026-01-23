using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterHealth : Health
{
    [SerializeField] private MonsterHealthBarUI MonsterHelathBarUI;

    private Monster monster;

    private void Awake()
    {
        monster = GetComponent<Monster>();
    }

    private void OnEnable()
    {
        if (MonsterHelathBarUI != null)
        {
            MonsterHelathBarUI.Bind(this);
        }
            
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (MonsterHelathBarUI != null)
        {
            MonsterHelathBarUI.UpdateUI();
        }
    }

    protected override void Die()
    {
        monster.NotifyDead();
    }

    public void ResetHealth(int maxHp)
    {
        Init(maxHp);

        if (MonsterHelathBarUI != null)
        {
            MonsterHelathBarUI.UpdateUI();
        }
    }
}
