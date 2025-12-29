using System.Collections.Generic;

public class RangedAttack : ITowerAttack
{
    private List<ITowerEffect> effects;
    private float projectileSpeed;

    public RangedAttack(List<ITowerEffect> effects, float projectileSpeed)
    {
        this.effects = effects;
        this.projectileSpeed = projectileSpeed;
    }

    public void Execute(Monster target)
    {
        // TODO: 투사체 생성
        foreach (var effect in effects)
        {
            effect.Apply(target);
        }
    }
}
