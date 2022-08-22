public class HeroHealthHandler
{
    private readonly HeroHealthView _heroHealthView;
    private readonly IHealth _heroHealth;

    public HeroHealthHandler(HeroHealthView heroHealthView, IHealth heroHealth, HeroPrefab heroPrefab)
    {
        _heroHealthView = heroHealthView;
        _heroHealth = heroHealth;
        
        _heroHealth.SetInitialHealth(20);
        _heroHealthView.Init(20);
        _heroHealthView.SetValue(_heroHealth.CurrentHealth);

        heroPrefab.OnGetDamage += ChangeHealth;
    }

    private void ChangeHealth(float value)
    {
        _heroHealth.GetDamage(value);
        _heroHealthView.SetValue(_heroHealth.CurrentHealth);
    }
}