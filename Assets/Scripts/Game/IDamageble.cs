using System;

public interface IDamageble
{
    public void GetDamage(float value);
    public event Action<float> OnDamage;
}