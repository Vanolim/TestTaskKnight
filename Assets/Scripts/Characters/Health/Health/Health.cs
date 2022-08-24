using System;

public class Health : IHealth
{
    private float _currentHealth;
    private float _maxHealth;
    
    public event Action OnEmpty;
    public event Action<float> OnChanged;

    public float CurrentHealth => _currentHealth;

    public void SetInitialHealth(float value)
    {
        _maxHealth = value;
        _currentHealth = _maxHealth;
    }

    public void GetDamage(float value)
    {
        if (_currentHealth - value <= 0)
        {
            _currentHealth = 0;
            OnEmpty?.Invoke();
        }
        else
        {
            _currentHealth -= value;
        }
        
        OnChanged?.Invoke(_currentHealth);
    }

    public void AddHealth(float value)
    {
        if(_currentHealth + value > _maxHealth)
            return;
        
        _currentHealth += value;
        OnChanged?.Invoke(_currentHealth);
    }
}