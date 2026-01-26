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

        // 1. 독 장판 생성
        if (poisonAreaPrefab != null)
        {
            GameObject area = Instantiate(poisonAreaPrefab, monster.transform.position, Quaternion.identity);

            if (area.TryGetComponent(out PoisonArea poisonArea))
            {
                poisonArea.Init(splashRadius, poisonAreaDuration, effects, monster.transform);
            }
        }

        // 2. 기본 Effect 적용
        foreach (var effect in effects)
            effect.Apply(monster);

        Destroy(gameObject);
    }
}
