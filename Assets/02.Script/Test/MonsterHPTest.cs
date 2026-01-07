using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHPTest : MonoBehaviour
{
    [SerializeField] private float damage = 10f;

    [SerializeField] private float castleDamage = 5f;
    [SerializeField] private CastleHealth castleHealth;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Monster[] monsters = FindObjectsOfType<Monster>();

            int hitCount = 0;

            foreach (var m in monsters)
            {
                if (m != null && m.gameObject.activeInHierarchy)
                {
                    m.TakeDamage(damage);
                    hitCount++;
                }
            }

            Debug.Log($"K 키 입력: 몬스터 {hitCount}마리에게 {damage} 데미지");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (castleHealth == null)
            {
                return;
            }

            castleHealth.TakeDamage(castleDamage);

            Debug.Log($"L 키 입력: 성에게 {castleDamage} 데미지 (현재 HP: {castleHealth.CurrentHp}/{castleHealth.MaxHp})");
        }
    }
}
