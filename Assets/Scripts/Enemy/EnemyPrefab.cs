using System;
using UnityEngine;

public class EnemyPrefab : MonoBehaviour, IDamageble
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _center;
    [SerializeField] private int _enemyLayer;
    [SerializeField] private HealthView _healthView;
    [SerializeField] private CharacterEventAttackDetector _characterEventAttackDetector;

    public event Action<float> OnDamage;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    public Animator Animator => _animator;
    public Transform Center => _center;
    public int EnemyLayer => _enemyLayer;
    public IHealthView HealthView => _healthView;
    public CharacterEventAttackDetector CharacterEventAttackDetector => _characterEventAttackDetector;


    public void GetDamage(float value) => OnDamage?.Invoke(value);
}