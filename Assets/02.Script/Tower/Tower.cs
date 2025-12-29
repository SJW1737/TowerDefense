using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerData data;

    private ITowerAttack attack;
    private float attackTimer;

    private void Start()
    {
        TowerFactory.SetupTower(this);
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= data.attackInterval)
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
        Collider[] hits = Physics.OverlapSphere(transform.position, data.range);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Monster monster))
            {
                return monster; // 가장 먼저 찾은 적
            }
        }

        return null;
    }

    public void SetAttack(ITowerAttack attack)
    {
        this.attack = attack;
    }
}
