using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerData data;

    public Transform firePoint;      // 발사 위치
    public GameObject projectilePrefab;

    private ITowerAttack attack;
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
        attackTimer += Time.deltaTime;

        float attackInterval = 1f / data.attackInterval;

        if (attackTimer >= attackInterval)
        {
            Monster target = FindTarget();

            if (target != null)
            {
                attackTimer = 0f;
                attack?.Execute(target);
            }
        }
    }

    private Monster FindTarget()
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
    }

    public bool TryUpgrade()
    {
        // 1. 강화 가능 상태인지
        if (!CanUpgrade)
            return false;

        // 2. 다음 강화 비용 계산
        int cost = GetNextUpgradeCost();
        if (cost < 0)
            return false;

        // 3. 골드 충분한지
        if (!GoldManager.Instance.Spend(cost))
            return false;

        // 4. 강화 횟수 증가
        ApplyUpgrade();

        Debug.Log($"{data.towerName} 강화 성공 " + $"({upgradeCount}/{data.maxUpgradeCount}, 비용 {cost})");

        return true;
    }

    public int GetNextUpgradeCost()
    {
        if (!CanUpgrade)
            return -1; // 강화 불가 상태

        return data.upgradeCosts[upgradeCount];
    }

    private void ApplyUpgrade()
    {
        upgradeCount++;

        // Beam 타워라면 스택당 데미지 +1
        if (attack is BeamAttack beamAttack)
        {
            beamAttack.IncreaseBeamDamagePerStack(1);
        }

        // TODO : 나중에 여기서
        // - 공격속도 증가
        // - 사거리 증가
        // - Tier2 진화
        // 전부 처리 가능
    }

    // 사거리 체크
    private void OnDrawGizmos()
    {
        if (data == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, data.range);
    }
}
