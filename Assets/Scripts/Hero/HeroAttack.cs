using UnityEngine;

public class HeroAttack
{
    private readonly IHeroStates _heroStates;
    private readonly Transform _hero;
    private readonly HeroMovement _heroMovement;
    private readonly int _heroLayer;
    
    private bool _enterAttackState;

    private const float DISTANCE_ATTACK = 2.2f;
    private const float DAMAGE_VALUE = 1f;

    public HeroAttack(IHeroStates heroStates, Transform hero, HeroMovement heroMovement, int heroLayer)
    {
        _heroStates = heroStates;
        _hero = hero;
        _heroMovement = heroMovement;
        _heroLayer = heroLayer;

        _heroStates.OnStateChanged += CheckAttackState;
    }

    private void CheckAttackState(States currentState)
    {
        if (_enterAttackState)
        {
            Attack();
            _enterAttackState = false;
        }
        
        if (currentState == States.Attack)
            _enterAttackState = true;
    }

    private void Attack()
    {
        if(FindTarget(out IDamageble target))
            target.GetDamage(DAMAGE_VALUE);
    }

    private bool FindTarget(out IDamageble target)
    {
        RaycastHit2D hit = Physics2D.Raycast(_hero.position, _heroMovement.CurrentDirection, DISTANCE_ATTACK, ~(1 << _heroLayer));
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