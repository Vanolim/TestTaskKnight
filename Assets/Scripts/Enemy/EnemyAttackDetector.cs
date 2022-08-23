public class EnemyAttackDetector : IUpdateble
{
    private readonly ICharacterStates _characterStates;
    private readonly ISetDirection _setDirection;
    private float _distanceAttack;

    public EnemyAttackDetector(ICharacterStates characterStates, ISetDirection setDirection)
    {
        _characterStates = characterStates;
        _setDirection = setDirection;
    }

    public void Init(float distanceAttack) => _distanceAttack = distanceAttack;

    public void UpdateState(float dt)
    {
        if (_characterStates.CurrentCharacterState == States.Run && IsTargetInAttackDistance())
            _characterStates.Transit(States.Attack);
    }

    private bool IsTargetInAttackDistance()
    {
        if (_setDirection.Direction.x >= 0)
        {
            return _setDirection.Direction.x <= _distanceAttack;
        }
        else
        {
            return _setDirection.Direction.x >= _distanceAttack;
        }
    }
}