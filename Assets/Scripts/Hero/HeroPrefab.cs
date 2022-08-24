using System;
using UnityEngine;

public class HeroPrefab : MonoBehaviour, IDamageble
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _center;
    [SerializeField] private LayerMask _ignoreLayer;
    [SerializeField] private CharacterEventAttackDetector _characterEventAttackDetector;

    public event Action<float> OnDamage;
    public event Action<IAddHealth> OnTriggerAddHealth;

    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    public Rigidbody2D RB => _rb;
    public Animator Animator => _animator;
    public Transform Center => _center;
    public LayerMask IgnoreLayer => _ignoreLayer;
    public CharacterEventAttackDetector CharacterEventAttackDetector => _characterEventAttackDetector;

    public void GetDamage(float value) => OnDamage?.Invoke(value);

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.TryGetComponent(out IAddHealth addHealth))
            OnTriggerAddHealth?.Invoke(addHealth);
    }
}