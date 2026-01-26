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

        monster.ApplyPoisonArea(poisonAreaPrefab.GetComponent<PoisonArea>(), splashRadius, poisonAreaDuration, effects);

        Destroy(gameObject);
    }
}
