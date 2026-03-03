using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Monster : MonoBehaviour
{
    public MonsterData MonsterData { get; private set; }
    public MiniBossData MiniBossData { get; private set; }

    private MonsterMovement monsterMovement;

    private MonsterHealth monsterHealth;
    private CastleHealth castleHealth;

    private PoisonArea poisonArea;

    private Animator animator;

    private MonsterBurn monsterBurn;

    public event Action<Monster> OnDeath;

    public bool IsDead { get; private set; }

    private void Awake()
    {
        monsterMovement = GetComponent<MonsterMovement>();

        monsterHealth = GetComponent<MonsterHealth>();
        castleHealth = FindObjectOfType<CastleHealth>();

        animator = GetComponent<Animator>();

        monsterBurn = GetComponent<MonsterBurn>();
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

        float damageBonus = RelicManager.Instance.GetValue(RelicEffectType.EnemyDamageTaken);

        float finalDamage = damage * (1f + damageBonus);

        monsterHealth.TakeDamage(finalDamage);
    }

    public void ApplySlow(float slowRatio, float duration)
    {
        if (!gameObject.activeInHierarchy) return;

        monsterMovement.ApplySlow(slowRatio, duration);
    }

    public void ApplyFrozen(float duration)
    {
        if (!gameObject.activeInHierarchy) return;

        monsterMovement.ApplyFrozen(duration);
    }

    public void SetPoisonArea(PoisonArea area)
    {
        poisonArea = area;
    }

    public void ClearPoison()
    {
        poisonArea = null;
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

        monsterBurn?.StopBurn();

        OnDeath?.Invoke(this);

        monsterMovement.ResetMovement();

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }

        if (MonsterData.monsterType == MonsterType.Boss || MonsterData.monsterType == MonsterType.MiniBoss)
        {
            SoundManager.Instance.PlaySFX("BossDead");
        }

        animator.SetTrigger("Die");
    }

    public void OnDie()
    {
        float goldMultiplier = DifficultyManager.Instance.GoldMultiplier;

        int rewardGold = Mathf.RoundToInt(MonsterData.rewardGold * goldMultiplier);

        GoldManager.Instance.Add(rewardGold);

        if (MonsterData.monsterType == MonsterType.Boss)
        {
            InGameSession.Instance.killBoss++;
        }

        if (MonsterData.monsterType == MonsterType.MiniBoss)
        {
            InGameSession.Instance.killMiniBoss++;
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
        OnDeath = null;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = true;
        }

        animator.ResetTrigger("Die");
        animator.Play("Walk");

        monsterMovement.ResetMovement();

        //체력
        float hpMultiplier = DifficultyManager.Instance.HpMultiplier;   //웨이브에 따른 체력 증가
        float relicHpReduce = RelicManager.Instance.GetValue(RelicEffectType.EnemyMaxHp);   //유물로 인한 최대체력 감소

        float finalHpFloat = MonsterData.maxHP * hpMultiplier * (1f - relicHpReduce);

        int finalHp = Mathf.RoundToInt(finalHpFloat);
        
        monsterHealth.ResetHealth(finalHp);

        //이동 속도
        float relicSpeedReduce = RelicManager.Instance.GetValue(RelicEffectType.EnemyMoveSpeed);

        float finalBaseSpeed = MonsterData.moveSpeed * (1f - relicSpeedReduce);

        monsterMovement.SetSpeed(finalBaseSpeed);
        monsterMovement.Setpath();

        //스프라이트 방향 초기화
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.flipX = false;
    }
}
