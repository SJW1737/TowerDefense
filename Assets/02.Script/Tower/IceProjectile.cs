using UnityEngine;

public class IceProjectile : Projectile
{
    [Header("Ice Settings")]
    public float slowRadius;
    public float iceAreaDuration;
    public GameObject iceAreaPrefab;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Monster monster))
            return;
        if (monster != target)
            return;
        if (monster.IsDead)
            return;

        // 1. 얼음 장판 생성
        if (iceAreaPrefab != null)
        {
            GameObject obj = ObjectPool.Instance.Get(iceAreaPrefab);

            IceArea area = obj.GetComponent<IceArea>();
            area.transform.position = monster.transform.position;
            area.Init(slowRadius, iceAreaDuration);
        }

        // 2. 데미지, 속박 적용
        if (effects != null)
        {
            foreach (var effect in effects)
                effect.Apply(monster);
        }

        ObjectPool.Instance.ReturnToPool(this);
    }
}
