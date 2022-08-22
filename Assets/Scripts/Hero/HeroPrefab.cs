using System;
using UnityEngine;

public class HeroPrefab : MonoBehaviour, IDamageble
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _center;
    [SerializeField] private int _heroLayer;

    public event Action<float> OnGetDamage;

    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    public Rigidbody2D RB => _rb;
    public Animator Animator => _animator;
    public Transform Center => _center;
    public int HeroLayer => _heroLayer;
    public void GetDamage(float value) => OnGetDamage?.Invoke(value);
}