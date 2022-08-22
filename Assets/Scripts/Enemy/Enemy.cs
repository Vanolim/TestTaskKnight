public class Enemy
{
    private readonly EnemyPrefab _enemyPrefab;
    private readonly ISetDirection _setDirection;
    private readonly IHealth _enemyHealth;
    private ICharacterStates _enemyStates;
    private EnemyMovement _enemyMovement;
    private CharacterAnimator _enemyAnimator;
    private CharacterAnimatorHandler _characterAnimatorHandler;
    private CharacterAttack _enemyAttack;
    private HealthHandler _healthHandler;
    
    public bool IsActive { get; private set; }

    public Enemy(EnemyPrefab enemyPrefab, ICharacterStates enemyStates, ISetDirection setDirection, IHealth enemyHealth)
    {
        _enemyPrefab = enemyPrefab;
        _enemyStates = enemyStates;
        _setDirection = setDirection;
        _enemyHealth = enemyHealth;

        Init(setDirection);
    }

    private void Init(ISetDirection setDirection)
    {
        _enemyMovement = new EnemyMovement(_enemyPrefab.transform, _enemyStates, _enemyPrefab.SpriteRenderer, setDirection);
        _enemyMovement.Init(moveSpeed: 7);

        _enemyAnimator = new CharacterAnimator(_enemyPrefab.Animator);
        _characterAnimatorHandler = new CharacterAnimatorHandler(_enemyAnimator, _enemyStates);
        _enemyAttack = new CharacterAttack(_enemyStates, _enemyPrefab.Center, setDirection, _enemyPrefab.EnemyLayer);

        _healthHandler = new HealthHandler(_enemyPrefab.HealthView, _enemyHealth, _enemyPrefab);
        _healthHandler.Init(initialHealth: 1);
        
        
        IDetectorFinishAnimation detectorFinishAnimation = new DetectorFinishAnimation(_enemyAnimator);
        StateResetter stateResetter = new StateResetter(detectorFinishAnimation, _enemyStates);
    }

    public void Activate()
    {
        IsActive = true;
        _enemyPrefab.gameObject.SetActive(true);
        _enemyStates.Transit(States.Run);
    }
    
    public void Deactivate()
    {
        IsActive = false;
        _enemyPrefab.gameObject.SetActive(false);
    }
}