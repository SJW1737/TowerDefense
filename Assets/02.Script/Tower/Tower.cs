using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerData data;
    private Monster currentTarget;

    [SerializeField] private GameObject rangePrefab;
    private GameObject rangeInstance;
    public float AttackRange => data.range;

    public Transform firePoint;      // 발사 위치

    private ITowerAttack attack;
    private ITickableAttack tickAttack;
    private List<ITowerEffect> effects;

    private float attackTimer;

    [SerializeField] private LayerMask monsterLayer;

    private int upgradeCount = 0;
    public int UpgradeCount => upgradeCount;
    public bool CanUpgrade => upgradeCount < data.maxUpgradeCount;

    private float rotationSpeed = 360f; // 초당 회전 각도

    private void Start()
    {
        TowerFactory.SetupTower(this);

        rangeInstance = Instantiate(rangePrefab, transform);
        rangeInstance.SetActive(false);

        UpdateRangeVisual();
    }

    private void Update()
    {
        tickAttack?.Tick(Time.deltaTime);

        attackTimer += Time.deltaTime;

        if (currentTarget == null || currentTarget.IsDead)
        {
            currentTarget = FindTarget();
        }

        if (currentTarget != null)
        {
            float dist = Vector2.Distance(transform.position, currentTarget.transform.position);

            if (dist <= data.range)
            {
                RotateToTarget(currentTarget);

                float attackInterval = data.GetAttackInterval(upgradeCount);

                if (attackTimer >= attackInterval)
                {
                    attackTimer = 0f;
                    attack?.Execute(currentTarget);
                }
            }

            else
            {
                currentTarget = null;
                MoveToStandby();
            }
        }

        else
        {
            MoveToStandby();
        }
    }

    public Monster FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, data.range, monsterLayer);

        Monster frontMost = null;
        float maxDistance = float.MinValue;

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Monster monster))
            {
                MonsterMovement move = monster.GetComponent<MonsterMovement>();
                if (move == null) continue;

                float dist = Vector2.Distance(transform.position, monster.transform.position);
                if (move.TravelDistance > maxDistance)
                {
                    maxDistance = move.TravelDistance;
                    frontMost = monster;
                }
            }
        }

        return frontMost;
    }

    private void RotateToTarget(Monster target)
    {
        Vector2 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRot = Quaternion.Euler(0f, 0f, angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * 10f);
    }

    private void MoveToStandby()
    {
        float currentZ = transform.eulerAngles.z;
        float newZ = Mathf.MoveTowardsAngle(currentZ, 0f, rotationSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0f, 0f, newZ);
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

        Debug.Log($"{data.towerName} 강화 완료 ({upgradeCount}/{data.maxUpgradeCount})");
        return true;
    }

    private void UpdateRangeVisual()
    {
        float diameter = AttackRange * 2f;
        rangeInstance.transform.localScale = new Vector3(diameter, diameter, 1f);
    }

    public void ShowRange(bool show)
    {
        rangeInstance.SetActive(show);
    }

    // 사거리 체크
    private void OnDrawGizmos()
    {
        if (data == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, data.range);
    }
}
