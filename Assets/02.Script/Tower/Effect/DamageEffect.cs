public class DamageEffect : ITowerEffect, IUpgradeableEffect
{
    private float baseDamage;
    private float damageGrowth;
    private int upgradeLevel;
    private float beamBonus;

    public DamageEffect(float baseDamage, float damageGrowth)
    {
        this.baseDamage = baseDamage;
        this.damageGrowth = damageGrowth;
    }

    public void OnUpgrade(int level)
    {
        upgradeLevel = level;
    }

    public void SetBeamBonus(float bonus)
    {
        beamBonus = bonus;
    }

    public float GetFinalDamage()
    {
        return baseDamage + (upgradeLevel * damageGrowth) + beamBonus;
    }

    public void Apply(Monster target)
    {
        target.TakeDamage(GetFinalDamage());
    }
}
