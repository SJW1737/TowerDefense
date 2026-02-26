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
        base.Init(null, speed, effects);

        this.targetPos = targetPos;
        this.explosionRadius = explosionRadius;

        totalDistance = Vector3.Distance(transform.position, targetPos);
    }

    protected override void Update()
    {
        if (totalDistance <= 0f)
            return;

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

    private void OnDisable()
    {
        targetPos = Vector3.zero;
        explosionRadius = 0f;
        totalDistance = 0f;

        StopAllCoroutines();
    }
}
