using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : Health
{
    [SerializeField] private MonsterData monsterData;

    private void Start()
    {
        Init(monsterData.maxHP);
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }
}
