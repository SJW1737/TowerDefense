using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeamAttack : ITowerAttack
{
    private readonly List<ITowerEffect> effects;

    private DamageEffect damageEffect;

    private Monster currentTarget;
    private float stackTimer;
    private int stackCount;

    private const float stackInterval = 0.2f;
    private const int maxStack = 10;

    public BeamAttack(List<ITowerEffect> effects)
    {
        this.effects = effects;
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

        damageEffect.SetBonusDamage(stackCount);

        foreach (var effect in effects)
        {
            effect.Apply(target);
        }
    }

    private void ResetStack()
    {
        stackTimer = 0f;
        stackCount = 0;
        damageEffect?.SetBonusDamage(0);
    }
}
