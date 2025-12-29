using System.Collections.Generic;

public class MeleeAttack : ITowerAttack
{
    private List<ITowerEffect> effects;

    public MeleeAttack(List<ITowerEffect> effects)
    {
        this.effects = effects;
    }

    public void Execute(Monster target)
    {
        // "근접 공격 방식" 여기에 등록
        // 타겟 찾고 타격
        foreach (var effect in effects)
        {
            effect.Apply(target);
        }
    }
}
