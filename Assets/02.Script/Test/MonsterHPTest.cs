using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHPTest : MonoBehaviour
{
    private Monster monster;

    private void Awake()
    {
        monster = FindObjectOfType<Monster>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("K 키 입력 몬스터 데미지");
            monster.TakeDamage(10);
        }
    }
}
