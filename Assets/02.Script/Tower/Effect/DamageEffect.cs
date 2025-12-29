using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : ITowerEffect
{
    private int damage;

    public DamageEffect(int damage)
    {
        this.damage = damage;
    }

    public void Apply(Monster target)
    {
        target.TakeDamage(damage);
    }
}
