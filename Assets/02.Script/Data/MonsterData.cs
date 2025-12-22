using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    Normal,     //기본
    Fast,       //체력이 낮고 빠른
    Tank,       //체력이 높고 느린
    Boss        //보스
}


[CreateAssetMenu(fileName = "MonsterData", menuName = "NewMonster")]
public class MonsterData : ScriptableObject
{
    [Header("몬스터 기본 데이터")]
    public string monsterName;          // 이름
    public MonsterType monsterType;     // 종류
    public string description;          // 설명
    public int maxHP;                   // 체력
    public int damage;                  // 타워에 주는 피해량
    public float moveSpeed;             // 이동속도
    public int rewardGold;              // 처치보상
}
