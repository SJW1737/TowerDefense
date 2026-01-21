using System.Collections.Generic;
using UnityEngine;

public class FireAreaAttack : ITickableAttack
{
    private readonly Tower tower;
    private readonly List<ITowerEffect> effects;

    private readonly GameObject firePrefab;
    private GameObject fireInstance;

    private bool isFireActive = false;
    private float noTargetTimer;
    private const float fireOffDelay = 0.3f; // 0.3초 여유

    public FireAreaAttack(Tower tower, List<ITowerEffect> effects, GameObject firePrefab)
    {
        this.tower = tower;
        this.effects = effects;
        this.firePrefab = firePrefab;

        if (firePrefab != null)
        {
            fireInstance = GameObject.Instantiate(firePrefab, tower.transform.position, Quaternion.identity, tower.transform);

            fireInstance.transform.localScale = Vector3.one * tower.data.range * 2f;

            fireInstance.SetActive(false); // 처음엔 OFF
        }
    }

    public void Tick(float deltaTime)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(tower.transform.position, tower.data.range, LayerMask.GetMask("Monster"));

        bool hasTarget = hits.Length > 0;

        if (hasTarget)
        {
            noTargetTimer = 0f;

            if (!isFireActive)
            {
                isFireActive = true;
                fireInstance.SetActive(true);
            }
        }
        else
        {
            noTargetTimer += deltaTime;

            if (isFireActive && noTargetTimer >= fireOffDelay)
            {
                isFireActive = false;
                fireInstance.SetActive(false);
            }

            return;
        }

        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent(out Monster monster))
                continue;

            foreach (var effect in effects)
                effect.Apply(monster);
        }
    }
}
