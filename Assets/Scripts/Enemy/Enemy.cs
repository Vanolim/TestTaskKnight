using System;
using UnityEngine;

public class Enemy : IUpdateble
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
    private DetectorFinishAnimation _detectorFinishAnimation;
    private EnemyAttackDetector _enemyAttackDetector;

    private float _damage = 1f;
    
    public bool IsActive { get; private set; }

    public event Action<Enemy> OnDie;

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
        _enemyStates.InitInitialState(States.Run);
        
        _enemyMovement = new EnemyMovement(_enemyPrefab.transform, _enemyStates, _enemyPrefab.SpriteRenderer, setDirection);
        _enemyMovement.Init(moveSpeed: 7);

        _enemyAnimator = new CharacterAnimator(_enemyPrefab.Animator);
        _characterAnimatorHandler = new CharacterAnimatorHandler(_enemyAnimator, _enemyStates);

        _enemyAttackDetector = new EnemyAttackDetector(_enemyStates, setDirection);
        _enemyAttack = new CharacterAttack(_enemyPrefab.CharacterEventAttackDetector, _enemyPrefab.Center, setDirection, _enemyPrefab.EnemyLayer);
        _enemyAttack.Init(distanceAttack: 2f, damage: _damage);
        _enemyAttackDetector.Init(distanceAttack: 2f);
        
        _healthHandler = new HealthHandler(_enemyPrefab.HealthView, _enemyHealth, _enemyPrefab, _enemyStates);
        _healthHandler.Init(initialHealth: 1);
        _healthHandler.OnDie += SetDieState;

        _detectorFinishAnimation = new DetectorFinishAnimation(_enemyAnimator);
        StateResetter stateResetter = new StateResetter(_detectorFinishAnimation, _enemyStates);
    }

    public void UpgradeDamage()
    {
        _enemyAttack.Init(distanceAttack: 2f, damage: _damage += 1f);
    }

    public void Activate()
    {
        Reset();
        IsActive = true;
        _enemyPrefab.gameObject.SetActive(true);
        _enemyStates.Transit(States.Run);
    }
    
    private void SetDieState()
    {
        _enemyStates.Transit(States.Die);
        _enemyStates.OnStateChanged += Die;
    }

    private void Die(States states)
    {
        if (states != States.Die)
        {
            _enemyStates.OnStateChanged -= Die;
            OnDie?.Invoke(this);
        }
    }

    public void UpdateState(float dt)
    {
        _enemyMovement.UpdateState(dt);
        _detectorFinishAnimation.UpdateState(dt);
        _enemyAttackDetector.UpdateState(dt);
    }

    private void Reset()
    {
        _characterAnimatorHandler.Reset();
        _healthHandler.Init(initialHealth: 1);
    }

    public void Deactivate()
    {
        IsActive = false;
        _enemyPrefab.gameObject.SetActive(false);
    }

    public void SetPosition(Vector2 enemySpawnerLeftSpawnPosition)
    {
        _enemyPrefab.transform.position = enemySpawnerLeftSpawnPosition;
    }
}