public class HealthHandler
{
    private readonly IHealthView _healthView;
    private readonly IHealth _characterHealth;
    private readonly IDamageble _character;

    public HealthHandler(IHealthView healthView, IHealth characterHealth, IDamageble character)
    {
        _healthView = healthView;
        _characterHealth = characterHealth;
        _character = character;

        character.OnDamage += ChangeHealth;
    }

    public void Init(int initialHealth)
    {
        _characterHealth.SetInitialHealth(initialHealth);
        _healthView.Init(initialHealth);
    }
    
    private void ChangeHealth(float value)
    {
        _characterHealth.GetDamage(value);
        _healthView.SetValue(_characterHealth.CurrentHealth);
    }
}