using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterData monsterData;

    private MonsterMovement monsterMovement;

    private void Awake()
    {
        monsterMovement = GetComponent<MonsterMovement>();
    }

    private void Start()
    {
        monsterMovement.SetSpeed(monsterData.moveSpeed);
        monsterMovement.Setpath();
    }
}
