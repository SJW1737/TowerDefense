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

    private void OnEnable()
    {
        monsterMovement.OnReachedEnd += OnArrivedAtCastle;
    }

    public void Activate()
    {
        ResetMonster();
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
        ReturnToPool();
    }

    public void OnDie()
    {
        int rewardGold = monsterData.rewardGold + DifficultyManager.Instance.GoldBonus;

        GoldManager.Instance.Add(rewardGold);

        if (monsterData.isBoss)
        {
            DifficultyManager.Instance.OnBossDefeated();
        }

        MonsterPoolManager.Instance.ReturnMonster(monsterData.monsterType, gameObject);
    }

    private void ReturnToPool()
    {
        MonsterPoolManager.Instance.ReturnMonster(monsterData.monsterType, gameObject);
    }

    private void OnDisable()
    {
        //이벤트 해제
        if (monsterMovement != null)
        {
            monsterMovement.OnReachedEnd -= OnArrivedAtCastle;
        }
    }

    public void ResetMonster()
    {
        float hpMultiplier = DifficultyManager.Instance.HpMultiplier;
        int finalHp = Mathf.RoundToInt(monsterData.maxHP * hpMultiplier);
        
        monsterHealth.ResetHealth(finalHp);
        monsterMovement.ResetMovement();

        monsterMovement.SetSpeed(monsterData.moveSpeed);
        monsterMovement.Setpath();
    }
}
