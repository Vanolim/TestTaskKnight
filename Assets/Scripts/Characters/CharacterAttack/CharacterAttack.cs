using UnityEngine;

public class CharacterAttack : IDisposable
{
    private readonly CharacterEventAttackDetector _characterEventAttackDetector;
    private readonly Transform _characterCenter;
    private readonly ISetDirection _setDirection;
    private readonly int _ignoreLayerMask;
    
    private bool _enterAttackState;
    private float _distanceAttack;
    private float _damage;

    public CharacterAttack(CharacterEventAttackDetector characterEventAttackDetector, Transform characterCenter, 
        ISetDirection setDirection, LayerMask ignoreLayer)
    {
        _characterEventAttackDetector = characterEventAttackDetector;
        _characterCenter = characterCenter;
        _setDirection = setDirection;
        _ignoreLayerMask = ~ignoreLayer;

        _characterEventAttackDetector.OnAttack += Attack;
    }

    public void Init(float distanceAttack, float damage)
    {
        _distanceAttack = distanceAttack;
        _damage = damage;
    }

    public void UpgradeDamage(float value) => _damage += value;

    private void Attack()
    {
        if(FindTarget(out IDamageble target))
            target.GetDamage(_damage);
    }

    private bool FindTarget(out IDamageble target)
    {
        RaycastHit2D hit = Physics2D.Raycast(_characterCenter.position, _setDirection.Direction, _distanceAttack, _ignoreLayerMask);
        if (hit.collider != null)
        {
            if (hit.transform.TryGetComponent(out IDamageble damageble))
            {
                target = damageble;
                return true;
            }
        }

        target = null;
        return false;
    }

    public void Dispose() => _characterEventAttackDetector.OnAttack -= Attack;
}