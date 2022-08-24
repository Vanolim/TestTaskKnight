using UnityEngine;

public class EnemyAttackDetector : IUpdateble
{
    private readonly ICharacterStates _characterStates;
    private readonly ISetDirection _setDirection;
    private readonly float _distanceAttack;
    private readonly float _miDistanceAttack;
    
    private const float VALUE_MIN_DAISTANCE_ATTACK = 0.5f;

    public EnemyAttackDetector(ICharacterStates characterStates, ISetDirection setDirection, float distanceAttack)
    {
        _distanceAttack = distanceAttack;
        _miDistanceAttack = _distanceAttack - VALUE_MIN_DAISTANCE_ATTACK;
        _characterStates = characterStates;
        _setDirection = setDirection;
    }

    public void UpdateState(float dt)
    {
        if (_characterStates.CurrentCharacterState == States.Run && IsTargetInAttackDistance())
            _characterStates.Transit(States.Attack);
    }

    private bool IsTargetInAttackDistance() => Mathf.Abs(_setDirection.Direction.x) <= GetRandomDistance();

    private float GetRandomDistance() => Random.Range(_miDistanceAttack, _distanceAttack);
}