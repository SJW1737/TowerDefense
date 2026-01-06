using System.Collections.Generic;
using System.Linq;

public static class TowerFactory
{
    public static void SetupTower(Tower tower)
    {
        TowerData data = tower.data;

        var effects = data.effects.Select(e => e.CreateEffect()).ToList();

        ITowerAttack attack = data.attackData.CreateAttack(tower, effects);

        tower.SetAttack(attack);
    }
}