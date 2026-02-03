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

        // 공격 방식 결정
        if (data is SummonTowerData summonData)
        {
            // 소환수 타워는 AttackData 무시
            tower.SetAttack(new SummonAttack(tower, summonData));
        }
        else
        {
            // 기존 타워
            ITowerAttack attack = data.attackData.CreateAttack(tower, effects);
            tower.SetAttack(attack);

            // Tick 공격
            if (data.attackData is ITickAttackData tickData)
            {
                tower.SetTickAttack(tickData.CreateTickAttack(tower, effects));
            }
        }
    }
}