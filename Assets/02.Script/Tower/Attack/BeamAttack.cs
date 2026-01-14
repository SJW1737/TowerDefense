using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeamAttack : ITowerAttack
{
    private readonly List<ITowerEffect> effects;

    private readonly float stackInterval;
    private readonly int maxStack;

    private int beamDamagePerStack;

    private DamageEffect damageEffect;

    private Monster currentTarget;
    private float stackTimer;
    private int stackCount;

    public BeamAttack(List<ITowerEffect> effects, float stackInterval, int maxStack, int beamDamagePerStack)
    {
        this.effects = effects;
        this.stackInterval = stackInterval;
        this.maxStack = maxStack;
        this.beamDamagePerStack = beamDamagePerStack;

        damageEffect = effects.OfType<DamageEffect>().FirstOrDefault();
    }

    public void Execute(Monster target)
    {
        if (target == null || damageEffect == null)
        {
            ResetStack();
            currentTarget = null;
            return;
        }

        if (target != currentTarget)
        {
            ResetStack();
            currentTarget = target;
        }

        stackTimer += Time.deltaTime;

        if (stackTimer >= stackInterval)
        {
            stackTimer = 0f;
            stackCount = Mathf.Min(stackCount + 1, maxStack);
        }

        damageEffect.SetBeamBonus(stackCount * beamDamagePerStack);

        foreach (var effect in effects)
        {
            effect.Apply(target);
        }
    }

    public void IncreaseBeamDamagePerStack(int amount)
    {
        beamDamagePerStack += amount;
    }

    private void ResetStack()
    {
        stackTimer = 0f;
        stackCount = 0;
        damageEffect?.SetBeamBonus(0);
    }
}
