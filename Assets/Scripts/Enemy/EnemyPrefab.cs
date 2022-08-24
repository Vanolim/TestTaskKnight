using System;
using UnityEngine;

public class EnemyPrefab : MonoBehaviour, IDamageble
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _center;
    [SerializeField] private LayerMask _ignoreLayer;
    [SerializeField] private HealthView _healthView;
    [SerializeField] private CharacterEventAttackDetector _characterEventAttackDetector;
    [SerializeField] private BoxCollider2D _boxCollider2D;

    public event Action<float> OnDamage;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    public Animator Animator => _animator;
    public Transform Center => _center;
    public LayerMask IgnoreLayer => _ignoreLayer;
    public IHealthView HealthView => _healthView;
    public CharacterEventAttackDetector CharacterEventAttackDetector => _characterEventAttackDetector;

    public void GetDamage(float value) => OnDamage?.Invoke(value);

    public void ActivateCollider() => _boxCollider2D.enabled = true;

    public void DeactivateCollider() => _boxCollider2D.enabled = false;
}