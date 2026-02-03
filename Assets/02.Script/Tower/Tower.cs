using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerData data;

    public Transform firePoint;      // 발사 위치

    private ITowerAttack attack;
    private ITickableAttack tickAttack;
    private List<ITowerEffect> effects;

    private float attackTimer;

    [SerializeField] private LayerMask monsterLayer;

    private int upgradeCount = 0;
    public int UpgradeCount => upgradeCount;
    public bool CanUpgrade => upgradeCount < data.maxUpgradeCount;

    private void Start()
    {
        TowerFactory.SetupTower(this);
    }

    private void Update()
    {
        tickAttack?.Tick(Time.deltaTime);

        attackTimer += Time.deltaTime;

        float attackInterval = data.GetAttackInterval(upgradeCount);

        if (attackTimer >= attackInterval)
        {
            Monster target = FindTarget();
            attackTimer = 0f;

            if (target != null)
            {
                attack?.Execute(target);
            }
        }
    }

    public Monster FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, data.range, monsterLayer);

        Monster closest = null;
        float minDist = float.MaxValue;

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Monster monster))
            {
                float dist = Vector2.Distance(transform.position, monster.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = monster;
                }
            }
        }

        return closest;
    }

    public void SetAttack(ITowerAttack attack)
    {
        this.attack = attack;

        if (attack is ITickableAttack tickable)
            this.tickAttack = tickable;
    }

    public void SetTickAttack(ITickableAttack tickAttack)
    {
        this.tickAttack = tickAttack;
    }

    public void SetEffects(List<ITowerEffect> effects)
    {
        this.effects = effects;
    }

    public T GetEffect<T>() where T : class, ITowerEffect
    {
        foreach (var effect in effects)
        {
            if (effect is T target)
                return target;
        }

        Debug.LogError($"{typeof(T).Name} effect not found on tower");
        return null;
    }

    public List<ITowerEffect> GetEffects()
    {
        return effects;
    }

    public bool TryUpgrade()
    {
        if (!CanUpgrade)
            return false;

        int cost = data.upgradeCosts[upgradeCount];

        if (!GoldManager.Instance.Spend(cost))
            return false;

        upgradeCount++;
        
        // 모든 Effect에 강화 전파
        foreach (var effect in effects)
        {
            if (effect is IUpgradeableEffect upgradeable)
            {
                upgradeable.OnUpgrade(upgradeCount);
            }
        }

        // Beam 특수 처리
        if (attack is BeamAttack beamAttack)
        {
            beamAttack.IncreaseBeamDamagePerStack(1);
        }

        // TODO : 나중에 여기서
        // - 공격속도 증가
        // - 사거리 증가
        // - Tier2 진화
        // 전부 처리 가능

        Debug.Log($"{data.towerName} 강화 완료 ({upgradeCount}/{data.maxUpgradeCount})");
        return true;
    }

    // 사거리 체크
    private void OnDrawGizmos()
    {
        if (data == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, data.range);
    }
}
