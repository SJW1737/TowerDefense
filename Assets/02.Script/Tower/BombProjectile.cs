using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : Projectile
{
    private Vector3 targetPos;
    private float explosionRadius;

    public GameObject bombAreaPrefab;
    public float maxScale;

    private float totalDistance;

    public void Init(Vector3 targetPos, float speed, float explosionRadius, List<ITowerEffect> effects)
    {
        this.target = null;
        this.targetPos = targetPos;
        this.speed = speed;
        this.explosionRadius = explosionRadius;
        this.effects = effects != null ? new List<ITowerEffect>(effects) : null;

        totalDistance = Vector3.Distance(transform.position, targetPos);

        isInitialized = true;
    }

    protected override void Update()
    {
        if (!isInitialized)
            return;

        if (totalDistance <= 0f)
        {
            ReturnToPool();
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        UpdateScale();

        if (Vector3.Distance(transform.position, targetPos) < 0.05f)
        {
            Explode();
        }
    }

    private void UpdateScale()
    {
        float dist = Vector3.Distance(transform.position, targetPos);
        float t = Mathf.Clamp01(1f - (dist / totalDistance));
        float scale = Mathf.Lerp(1f, maxScale, t);
        transform.localScale = Vector3.one * scale;
    }

    private void Explode()
    {
        GameObject obj = ObjectPool.Instance.Get(bombAreaPrefab);

        BombArea area = obj.GetComponent<BombArea>();
        area.transform.position = transform.position;
        area.Init(explosionRadius, effects);

        ReturnToPool();
    }

    protected override void ReturnToPool()
    {
        isInitialized = false;
        effects = null;
        speed = 0f;
        targetPos = Vector3.zero;
        explosionRadius = 0f;
        totalDistance = 0f;

        ObjectPool.Instance.ReturnToPool(this);
    }
}
