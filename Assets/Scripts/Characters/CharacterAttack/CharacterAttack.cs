using UnityEngine;

public class CharacterAttack
{
    private readonly Transform _characterCenter;
    private readonly ISetDirection _setDirection;
    private readonly int _characterLayer;
    
    private bool _enterAttackState;
    private float _distanceAttack;
    private float _damage;

    public CharacterAttack(ICharacterStates characterStates, Transform characterCenter, ISetDirection setDirection, int characterLayer)
    {
        _characterCenter = characterCenter;
        _setDirection = setDirection;
        _characterLayer = characterLayer;

        characterStates.OnStateChanged += CheckAttackState;
    }

    public void Init(float distanceAttack, float damage)
    {
        _distanceAttack = distanceAttack;
        _damage = damage;
    }
    
    private void CheckAttackState(States currentCharacterState)
    {
        if (_enterAttackState)
        {
            Attack();
            _enterAttackState = false;
        }
        
        if (currentCharacterState == States.Attack)
            _enterAttackState = true;
    }

    private void Attack()
    {
        if(FindTarget(out IDamageble target))
            target.GetDamage(_damage);
    }

    private bool FindTarget(out IDamageble target)
    {
        RaycastHit2D hit = Physics2D.Raycast(_characterCenter.position, _setDirection.MoveDirection, _distanceAttack, ~(1 << _characterLayer));
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
}