using System.Collections.Generic;

public class SingleTargetAttack : ITowerAttack
{
    private readonly List<ITowerEffect> effects;

    public SingleTargetAttack(List<ITowerEffect> effects)
    {
        this.effects = effects;
    }

    public void Execute(Monster target)
    {
        foreach (var effect in effects)
            effect.Apply(target);
    }
}
