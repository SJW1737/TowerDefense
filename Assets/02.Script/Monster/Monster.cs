using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterData monsterData;

    private MonsterMovement monsterMovement;

    private MonsterHealth monsterHealth;
    private CastleHealth castleHealth;

    private void Awake()
    {
        monsterMovement = GetComponent<MonsterMovement>();

        monsterHealth = GetComponent<MonsterHealth>();
        castleHealth = FindObjectOfType<CastleHealth>();
    }

    private void Start()
    {
        monsterMovement.SetSpeed(monsterData.moveSpeed);
        monsterMovement.Setpath();

        monsterMovement.OnReachedEnd += OnArrivedAtCastle;
    }

    public void TakeDamage(int damage)
    {
        monsterHealth.TakeDamage(damage);
    }

    public void ApplySlow(float slowRatio, float duration)
    {
        monsterMovement.ApplySlow(slowRatio, duration);
    }

    private void OnArrivedAtCastle()
    {
        Debug.Log("몬스터 도착 및 성 체력 감소");
        //성 체력 감소
        castleHealth.TakeDamage(monsterData.damage);
        //몬스터 제거
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        //이벤트 해제
        if (monsterMovement != null)
        {
            monsterMovement.OnReachedEnd -= OnArrivedAtCastle;
        }
    }
}
