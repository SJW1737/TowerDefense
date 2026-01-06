using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public int MaxHp { get; protected set; }
    public int CurrentHp { get; protected set; }

    public virtual void Init(int maxHp)
    {
        MaxHp = maxHp;
        CurrentHp = maxHp;
    }

    public virtual void TakeDamage(int damage)
    {
        TakeDamage((float)damage);
    }

    public virtual void TakeDamage(float damage)
    {
        CurrentHp -= Mathf.RoundToInt(damage);
        CurrentHp = Mathf.Max(CurrentHp, 0);

        if (CurrentHp <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();
}
