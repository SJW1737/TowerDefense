using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerData data;

    public Transform firePoint;      // 발사 위치
    public GameObject projectilePrefab;

    private ITowerAttack attack;
    private float attackTimer;

    [SerializeField] private LayerMask monsterLayer;

    private void Start()
    {
        TowerFactory.SetupTower(this);
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;

        float attackInterval = 1f / data.attackSpeed;

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

    // 사거리 체크
    private void OnDrawGizmosSelected()
    {
        if (data == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, data.range);
    }
}
