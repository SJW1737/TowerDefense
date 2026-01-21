using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAreaAttack : ITickableAttack
{
    private readonly Tower tower;
    private readonly List<ITowerEffect> effects;
    private readonly GameObject fireInstance; // 씬에 생성된 인스턴스

    private float tickTimer;
    private bool isAttacking;

    private float stopTimer; // 공격 종료 딜레이용
    private const float stopDelay = 0.3f;

    public FireAreaAttack(Tower tower, List<ITowerEffect> effects, GameObject firePrefab)
    {
        this.tower = tower;
        this.effects = effects;

        // Prefab을 씬에 Instantiate
        fireInstance = GameObject.Instantiate(firePrefab);
        fireInstance.SetActive(false);

        // 타워 자식으로 붙이고 위치 초기화
        fireInstance.transform.SetParent(tower.transform);
        fireInstance.transform.localPosition = Vector3.zero;
        fireInstance.transform.localScale = new Vector3(tower.data.range * 2f, tower.data.range * 2f, 0f);
    }

    public void Tick(float deltaTime)
    {
        tickTimer += deltaTime;

        Collider2D[] hits = Physics2D.OverlapCircleAll(tower.transform.position, tower.data.range, LayerMask.GetMask("Monster"));

        bool hasTarget = false;

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Monster monster))
            {
                hasTarget = true;
                break;
            }
        }

        if (hasTarget)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                fireInstance.SetActive(true); // 공격 시작 시 켬
            }

            stopTimer = 0f;
        }
        else if (isAttacking)
        {
            stopTimer += deltaTime;

            if (stopTimer >= stopDelay)
            {
                isAttacking = false;
                fireInstance.SetActive(false);
                tickTimer = 0f;
                stopTimer = 0f;
            }

            return;
        }

        float interval = Mathf.Max(0.01f, tower.data.GetAttackInterval(tower.UpgradeCount));

        if (tickTimer >= interval)
        {
            tickTimer -= interval;

            foreach (var hit in hits)
            {
                if (!hit.TryGetComponent(out Monster monster)) 
                    continue;

                foreach (var effect in effects)
                    effect.Apply(monster);
            }
        }
    }
}
