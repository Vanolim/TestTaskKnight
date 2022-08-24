using System;
using UnityEngine;

public class HealthHandler : IDisposable
{
    private readonly IHealthView _healthView;
    private readonly IHealth _characterHealth;
    private readonly IDamageble _character;
    private readonly ICharacterStates _characterStates;

    public event Action OnDie;

    public HealthHandler(IHealthView healthView, IHealth characterHealth, IDamageble character, ICharacterStates characterStates)
    {
        _healthView = healthView;
        _characterHealth = characterHealth;
        _character = character;
        _characterStates = characterStates;

        _character.OnDamage += RemoveHealth;
        _characterHealth.OnEmpty += Die;
    }

    public void Init(int initialHealth)
    {
        _characterHealth.SetInitialHealth(initialHealth);
        _healthView.Init(initialHealth);
    }
    
    private void RemoveHealth(float value)
    {
        _characterStates.Transit(States.GetDamage);
        _characterHealth.GetDamage(value);
        _healthView.SetValue(_characterHealth.CurrentHealth);
    }

    public void AddHealth(IAddHealth addHealth)
    {
        _characterHealth.AddHealth(addHealth.AddHealth());
        _healthView.SetValue(_characterHealth.CurrentHealth);
    }

    private void Die() => OnDie?.Invoke();

    public void Dispose()
    {
        _character.OnDamage -= RemoveHealth;
        _characterHealth.OnEmpty -= Die;
    }
}