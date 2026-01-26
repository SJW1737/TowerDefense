using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : MonoBehaviour
{
    private Vector3 targetPos;
    private float speed;
    private float explosionRadius;
    private List<ITowerEffect> effects;

    public GameObject bombAreaPrefab;
    public float maxScale;

    private float totalDistance;

    public void Init(Vector3 targetPos, float speed, float explosionRadius, List<ITowerEffect> effects)
    {
        this.targetPos = targetPos;
        this.speed = speed;
        this.explosionRadius = explosionRadius;
        this.effects = effects;

        totalDistance = Vector3.Distance(transform.position, targetPos);
    }

    private void Update()
    {
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
        float t = 1f - (dist / totalDistance);
        float scale = Mathf.Lerp(1f, maxScale, t);
        transform.localScale = Vector3.one * scale;
    }

    private void Explode()
    {
        GameObject area = Instantiate(bombAreaPrefab, transform.position, Quaternion.identity);

        if (area.TryGetComponent(out BombArea bombArea))
        {
            bombArea.Init(explosionRadius, effects);
        }

        Destroy(gameObject);
    }
}
