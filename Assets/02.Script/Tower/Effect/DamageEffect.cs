public class DamageEffect : ITowerEffect, IUpgradeableEffect
{
    private int baseDamage;
    private int damageGrowth;
    private int upgradeLevel;
    private int beamBonus;

    public DamageEffect(int baseDamage, int damageGrowth)
    {
        this.baseDamage = baseDamage;
        this.damageGrowth = damageGrowth;
    }

    public void OnUpgrade(int level)
    {
        upgradeLevel = level;
    }

    public void SetBeamBonus(int bonus)
    {
        beamBonus = bonus;
    }

    public int GetFinalDamage()
    {
        return baseDamage + (upgradeLevel * damageGrowth) + beamBonus;
    }

    public void Apply(Monster target)
    {
        target.TakeDamage(GetFinalDamage());
    }
}
