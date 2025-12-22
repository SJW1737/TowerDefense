public static class TowerAttackFactory
{
    public static ITowerAttack Create(Tower tower, TowerType type)
    {
        return type switch
        {
            TowerType.Melee => new MeleeAttack(tower),
            TowerType.Ranged => new RangedAttack(tower),
            TowerType.Debuff => new DebuffAttack(tower),
            _ => null
        };
    }
}
