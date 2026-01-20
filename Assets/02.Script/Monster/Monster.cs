using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterData MonsterData { get; private set; }
    public MiniBossData MiniBossData { get; private set; }

    private MonsterMovement monsterMovement;
    private MonsterStatusEffect statusEffect;

    private MonsterHealth monsterHealth;
    private CastleHealth castleHealth;

    public bool IsDead { get; private set; }

    private void Awake()
    {
        monsterMovement = GetComponent<MonsterMovement>();
        statusEffect = GetComponent<MonsterStatusEffect>();

        monsterHealth = GetComponent<MonsterHealth>();
        castleHealth = FindObjectOfType<CastleHealth>();
    }

    private void OnEnable()
    {
        monsterMovement.OnReachedEnd += OnArrivedAtCastle;
    }

    public void Init(MonsterData monsterData, MiniBossData miniBossData = null)
    {
        MonsterData = monsterData;
        MiniBossData = miniBossData;
    }
    public void Activate()
    {
        if (MonsterData == null) return;

        ResetMonster();
    }

    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        monsterHealth.TakeDamage(damage);
    }

    public void ApplySlow(float slowRatio, float duration)
    {
        if (!gameObject.activeInHierarchy) return;

        monsterMovement.ApplySlow(slowRatio, duration);
    }

    public void ApplyDot(float damagePerTick, float duration, float interval)
    {
        statusEffect.ApplyDot(damagePerTick, duration, interval);
    }

    private void OnArrivedAtCastle()
    {
        if (IsDead) return;
        //성 체력 감소
        castleHealth.TakeDamage(MonsterData.damage);
        //몬스터 제거
        ReturnToPool();
    }

    public void NotifyDead()
    {
        if (IsDead) return;

        IsDead = true;
        OnDie();
    }

    public void OnDie()
    {
        monsterMovement.ResetMovement();

        int rewardGold = MonsterData.rewardGold + DifficultyManager.Instance.GoldBonus;

        GoldManager.Instance.Add(rewardGold);

        if (MonsterData.monsterType == MonsterType.Boss)
        {
            DifficultyManager.Instance.OnBossDefeated();
        }

        ReturnToPool();
    }

    private void ReturnToPool()
    {
        //미니보스
        if (MonsterData.monsterType == MonsterType.MiniBoss)
        {
            MonsterPoolManager.Instance.ReturnMiniBoss(this);
        }
        else//일반 몬스터
        {
            MonsterPoolManager.Instance.ReturnMonster(this);
        }
    }

    private void OnDisable()
    {
        //이벤트 해제
        monsterMovement.OnReachedEnd -= OnArrivedAtCastle;
    }

    public void ResetMonster()
    {
        IsDead = false;

        float hpMultiplier = DifficultyManager.Instance.HpMultiplier;
        int finalHp = Mathf.RoundToInt(MonsterData.maxHP * hpMultiplier);
        
        monsterHealth.ResetHealth(finalHp);
        monsterMovement.ResetMovement();

        monsterMovement.SetSpeed(MonsterData.moveSpeed);
        monsterMovement.Setpath();
    }
}
