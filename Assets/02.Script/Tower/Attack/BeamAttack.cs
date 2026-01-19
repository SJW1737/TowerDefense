using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeamAttack : ITowerAttack, ITickableAttack
{
    private readonly List<ITowerEffect> effects;

    private readonly float stackInterval;
    private readonly int maxStack;

    private int beamDamagePerStack;

    private DamageEffect damageEffect;

    private Monster currentTarget;
    private float stackTimer;
    private int stackCount;

    private float damageTimer;
    private const float damageInterval = 0.2f;


    private Beam beam;
    private GameObject beamPrefab;
    private Transform firePoint;

    public BeamAttack(List<ITowerEffect> effects, float stackInterval, int maxStack, int beamDamagePerStack, GameObject beamPrefab, Transform firePoint)
    {
        this.effects = effects;
        this.stackInterval = stackInterval;
        this.maxStack = maxStack;
        this.beamDamagePerStack = beamDamagePerStack;
        this.beamPrefab = beamPrefab;
        this.firePoint = firePoint;

        damageEffect = effects.OfType<DamageEffect>().FirstOrDefault();
    }

    public void Execute(Monster target)
    {
        if (damageEffect == null)
            return;

        if (target == null || target.IsDead)
            return;

        if (target != currentTarget)
        {
            ResetState();
            currentTarget = target;
            CreateBeam(target);
        }
    }

    public void Tick(float deltaTime)
    {
        if (currentTarget == null || currentTarget.IsDead)
        {
            ClearBeam();
            ResetState();
            currentTarget = null;
            return;
        }

        // 스택 처리
        stackTimer += deltaTime;
        if (stackTimer >= stackInterval)
        {
            stackTimer = 0f;
            stackCount = Mathf.Min(stackCount + 1, maxStack);
            damageEffect.SetBeamBonus(stackCount * beamDamagePerStack);
        }

        // 데미지 처리
        damageTimer += deltaTime;
        if (damageTimer >= damageInterval)
        {
            damageTimer = 0f;

            foreach (var effect in effects)
                effect.Apply(currentTarget);
        }
    }

    public void IncreaseBeamDamagePerStack(int amount)
    {
        beamDamagePerStack += amount;
    }

    private void ResetState()
    {
        stackTimer = 0f;
        damageTimer = 0f;
        stackCount = 0;
        damageEffect?.SetBeamBonus(0);
    }

    private void CreateBeam(Monster target)
    {
        ClearBeam();

        GameObject obj = GameObject.Instantiate(beamPrefab);
        beam = obj.GetComponent<Beam>();
        beam.Init(firePoint, target.transform);
    }

    private void ClearBeam()
    {
        if (beam != null)
            GameObject.Destroy(beam.gameObject);

        beam = null;
    }
}
