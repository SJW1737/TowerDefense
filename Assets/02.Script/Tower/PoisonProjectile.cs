using UnityEngine;

public class PoisonProjectile : Projectile
{
    [Header("Poison Settings")]
    public float splashRadius;
    public float poisonAreaDuration;
    public GameObject poisonAreaPrefab;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Monster monster))
            return;
        if (monster != target)
            return;

        SpawnPoisonArea(monster);

        ReturnToPool();
    }

    private void SpawnPoisonArea(Monster monster)
    {
        GameObject obj = ObjectPool.Instance.Get(poisonAreaPrefab);

        PoisonArea area = obj.GetComponent<PoisonArea>();
        area.transform.position = monster.transform.position;

        area.Init(monster, splashRadius, poisonAreaDuration, effects);

        monster.SetPoisonArea(area);
    }
}
