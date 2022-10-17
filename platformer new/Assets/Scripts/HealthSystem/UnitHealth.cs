public class UnitHealth
{
    public int Health { get; set; }
    public int MaxHealth { get; set; }

    public UnitHealth(int health,int maxHealth)
    {
        Health = health;
        MaxHealth = maxHealth;
    }

    public void DmgUnit(int dmgAmount)
    {
        if (Health > 0)
            Health -= dmgAmount;
    }

    public void HealUnit(int healAmount)
    {
        if (Health < MaxHealth)
            Health += healAmount;
        if (Health > MaxHealth)
            Health = MaxHealth;
    }
}
