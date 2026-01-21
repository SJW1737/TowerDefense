using System.Collections.Generic;

public static class TowerFactory
{
    public static void SetupTower(Tower tower)
    {
        TowerData data = tower.data;

        var effects = new List<ITowerEffect>();

        tower.SetEffects(effects); // 먼저 빈 리스트 연결

        foreach (var effectData in data.effects)
        {
            effects.Add(effectData.CreateEffect(tower));
        }

        ITowerAttack attack = data.attackData.CreateAttack(tower, effects);

        tower.SetAttack(attack);

        // Tick 전용 공격 처리
        if (data.attackData is ITickAttackData tickData)
        {
            tower.SetTickAttack(tickData.CreateTickAttack(tower, effects));
        }

    }
}