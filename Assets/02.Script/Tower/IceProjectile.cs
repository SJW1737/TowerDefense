using System.Collections;
using System.Collections.Generic;
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

        // 1. 얼음 장판 생성
        if (iceAreaPrefab != null)
        {
            GameObject area = Instantiate(iceAreaPrefab, monster.transform.position, Quaternion.identity);

            if (area.TryGetComponent(out IceArea iceArea))
            {
                iceArea.Init(slowRadius, iceAreaDuration);
            }
        }

        // 2. 데미지, 속박 적용
        foreach (var effect in effects)
            effect.Apply(monster);

        Destroy(gameObject);
    }
}
