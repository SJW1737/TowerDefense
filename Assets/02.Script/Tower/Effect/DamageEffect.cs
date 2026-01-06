public class DamageEffect : ITowerEffect
{
    private int baseDamage;
    private int bonusDamage;

    public DamageEffect(int baseDamage)
    {
        this.baseDamage = baseDamage;
    }

    public void SetBonusDamage(int bonus)
    {
        bonusDamage = bonus;
    }

    public void Apply(Monster target)
    {
        target.TakeDamage(baseDamage + bonusDamage);
    }
}
