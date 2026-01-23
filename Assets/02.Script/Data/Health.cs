using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public float MaxHp { get; protected set; }
    public float CurrentHp { get; protected set; }

    public virtual void Init(float maxHp)
    {
        MaxHp = maxHp;
        CurrentHp = maxHp;
    }

    public virtual void TakeDamage(float damage)
    {
        CurrentHp -= damage;
        CurrentHp = Mathf.Max(CurrentHp, 0);

        if (CurrentHp <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();
}
