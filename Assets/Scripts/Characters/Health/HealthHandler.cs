using System;
using UnityEngine;

public class HealthHandler
{
    private readonly IHealthView _healthView;
    private readonly IHealth _characterHealth;
    private readonly ICharacterStates _characterStates;

    public event Action OnDie;

    public HealthHandler(IHealthView healthView, IHealth characterHealth, IDamageble character, ICharacterStates characterStates)
    {
        _healthView = healthView;
        _characterHealth = characterHealth;
        _characterStates = characterStates;

        character.OnDamage += ChangeHealth;
        _characterHealth.OnEmpty += Die;
    }

    public void Init(int initialHealth)
    {
        _characterHealth.SetInitialHealth(initialHealth);
        _healthView.Init(initialHealth);
    }
    
    private void ChangeHealth(float value)
    {
        _characterStates.Transit(States.GetDamage);
        _characterHealth.GetDamage(value);
        _healthView.SetValue(_characterHealth.CurrentHealth);
    }

    private void Die()
    {
        OnDie?.Invoke();
    }
}