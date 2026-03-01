using UnityEngine;

public class SummonUnit : MonoBehaviour
{
    private Tower ownerTower;
    private Monster target;

    private TowerData towerData;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float projectileSpeed;

    private float rotationSpeed = 360f;   // 초당 회전 각도
    private float moveSpeed = 5f;         // 대기 위치 복귀 속도

    private float timer;

    private int slotIndex;
    private Vector3 standbyPosition;

    public void Initialize(Tower tower, Monster target)
    {
        this.ownerTower = tower;
        this.target = target;

        this.towerData = tower.data;
        timer = 0f;
    }

    private void Update()
    {
        if (ownerTower == null)
            return;

        if (target == null || target.IsDead)
        {
            MoveToStandby();
            FindNewTarget();
            return;
        }

        float dist = Vector2.Distance(transform.position, target.transform.position);

        if (dist > towerData.range)
        {
            MoveToStandby();
            return;
        }

        RotateToTarget();

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
        if (target == null || target.IsDead)
            return;

        GameObject obj = ObjectPool.Instance.Get(projectilePrefab.gameObject);

        obj.transform.position = firePoint.position;
        obj.transform.rotation = firePoint.rotation;

        Projectile proj = obj.GetComponent<Projectile>();

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
        transform.position = Vector3.MoveTowards(transform.position, standbyPosition, moveSpeed * Time.deltaTime);

        float currentZ = transform.eulerAngles.z;
        float newZ = Mathf.MoveTowardsAngle(currentZ, 180f, rotationSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0f, 0f, newZ);
    }

    private void RotateToTarget()
    {
        if (target == null)
            return;

        Vector2 dir = target.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;

        float currentZ = transform.eulerAngles.z;
        float newZ = Mathf.MoveTowardsAngle(currentZ, targetAngle, rotationSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0f, 0f, newZ);
    }
}
