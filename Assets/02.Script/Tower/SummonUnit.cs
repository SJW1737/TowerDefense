using UnityEngine;

public class SummonUnit : MonoBehaviour
{
    private Tower ownerTower;
    private Monster target;

    private TowerData towerData;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float projectileSpeed;

    private float timer;

    private int slotIndex;
    private Vector3 standbyPosition;

    public void Initialize(Tower tower, Monster target)
    {
        this.ownerTower = tower;
        this.target = target;

        this.towerData = tower.data;
    }

    private void Update()
    {
        if (ownerTower == null)
            return;

        if (target == null || !target.gameObject.activeSelf)
        {
            MoveToStandby();
            FindNewTarget();
            return;
        }

        RotateToTarget();

        float dist = Vector2.Distance(transform.position, target.transform.position);
        if (dist > towerData.range)
            return;

        timer += Time.deltaTime;

        float attackInterval = ownerTower.data.GetAttackInterval(ownerTower.UpgradeCount);

        if (timer >= attackInterval)
        {
            timer = 0f;
            Fire();
        }
    }

    private void Fire()
    {
        Projectile proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        proj.Init(target, projectileSpeed, ownerTower.GetEffects());
    }

    private void FindNewTarget()
    {
        if (ownerTower == null)
            return;

        target = ownerTower.FindTarget();
    }

    public void SetSlot(int index, Vector3 pos)
    {
        slotIndex = index;
        standbyPosition = pos;
    }

    private void MoveToStandby()
    {
        transform.position = Vector3.Lerp(transform.position, standbyPosition, Time.deltaTime * 5f);

        transform.rotation = Quaternion.Euler(0, 0, 90f);
    }

    private void RotateToTarget()
    {
        if (target == null)
            return;

        Vector2 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
