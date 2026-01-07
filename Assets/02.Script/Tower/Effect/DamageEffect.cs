public class DamageEffect : ITowerEffect
{
    private int baseDamage;
    private int upgradeLevel;
    private int beamBonus;

    public DamageEffect(int baseDamage)
    {
        this.baseDamage = baseDamage;
    }

    public void SetUpgradeLevel(int level)
    {
        upgradeLevel = level;
    }

    public void SetBeamBonus(int bonus)
    {
        beamBonus = bonus;
    }

    public int GetFinalDamage(int damageGrowth)
    {
        return baseDamage + (upgradeLevel * damageGrowth) + beamBonus;
    }

    public void Apply(Monster target)
    {
        target.TakeDamage(baseDamage + beamBonus);
    }
}
