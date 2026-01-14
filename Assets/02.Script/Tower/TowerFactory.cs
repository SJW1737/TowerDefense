using System.Collections.Generic;
using System.Linq;

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
        tower.SetEffects(effects);
    }
}