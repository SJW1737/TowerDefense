using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterData MonsterData { get; private set; }
    public MiniBossData MiniBossData { get; private set; }

    private MonsterMovement monsterMovement;

    private MonsterHealth monsterHealth;
    private CastleHealth castleHealth;

    private PoisonArea poisonArea;

    public bool IsDead { get; private set; }

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

    public void ApplyPoisonArea(PoisonArea prefab, float radius, float duration, List<ITowerEffect> effects)
    {
        if (poisonArea == null)
        {
            poisonArea = Instantiate(prefab, transform.position, Quaternion.identity);

            poisonArea.Init(this, radius, duration, effects);
        }
        else
        {
            poisonArea.Refresh(duration);
        }
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

            DailyMissionManager.Instance.AddProgress(DailyMissionType.KillBoss);
        }

        if (MonsterData.monsterType == MonsterType.MiniBoss)
        {
            DailyMissionManager.Instance.AddProgress(DailyMissionType.KillMiniBoss);
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

        monsterMovement.ResetMovement();

        //체력
        float hpMultiplier = DifficultyManager.Instance.HpMultiplier;   //보스 처치로 인한 체력 배율 증가
        float relicHpReduce = RelicManager.Instance.GetValue(RelicEffectType.EnemyMaxHp);   //유물로 인한 최대체력 감소

        float finalHpFloat = MonsterData.maxHP * hpMultiplier * (1f - relicHpReduce);

        int finalHp = Mathf.RoundToInt(finalHpFloat);
        
        monsterHealth.ResetHealth(finalHp);

        //이동 속도
        float relicSpeedReduce = RelicManager.Instance.GetValue(RelicEffectType.EnemyMoveSpeed);

        float finalBaseSpeed = MonsterData.moveSpeed * (1f - relicSpeedReduce);

        monsterMovement.SetSpeed(finalBaseSpeed);
        monsterMovement.Setpath();
    }
}
