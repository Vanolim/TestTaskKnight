using System;
using UnityEngine;

public class Heart : MonoBehaviour, IAddHealth
{
    [SerializeField] private float _valueAddHealth;

    public event Action<Heart> OnWorked;

    public float AddHealth()
    {
        OnWorked?.Invoke(this);
        return _valueAddHealth;
    }

    public void Destroyed() => Destroy(gameObject);
}