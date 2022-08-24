using System;
using UnityEngine;

public class Enemy : IUpdateble
{
    private readonly IHealth _enemyHealth;
    private readonly ICharacterStates _enemyStates;
    private readonly EnemyMovement _enemyMovement;
    private readonly CharacterAnimatorHandler _characterAnimatorHandler;
    private readonly HealthHandler _healthHandler;
    private readonly DetectorFinishAnimation _detectorFinishAnimation;
    private readonly EnemyAttackDetector _enemyAttackDetector;
    private CharacterAnimator _enemyAnimator;
    private EnemyDataPreset _enemyDataPreset;

    public EnemyPrefab EnemyPrefab { get; }
    public int Worth => _enemyDataPreset.Worth;
    public CharacterAttack EnemyAttack { get; private set;}
    public bool IsActive { get; private set; }

    public event Action<Enemy> OnDie;

    public Enemy(EnemyPrefab enemyPrefab, ICharacterStates enemyStates, ISetDirection setDirection, 
        IHealth enemyHealth, EnemyDataPreset enemyDataPreset)
    {
        EnemyPrefab = enemyPrefab;
        _enemyStates = enemyStates;
        _enemyHealth = enemyHealth;
        _enemyDataPreset = enemyDataPreset;
        
        _enemyMovement = new EnemyMovement(EnemyPrefab.transform, _enemyStates, EnemyPrefab.SpriteRenderer, setDirection);
        _enemyAnimator = new CharacterAnimator(EnemyPrefab.Animator);
        _characterAnimatorHandler = new CharacterAnimatorHandler(_enemyAnimator, _enemyStates, isUseRandomAttack: false);
        _enemyAttackDetector = new EnemyAttackDetector(_enemyStates, setDirection, _enemyDataPreset.DistanceAttack);
        EnemyAttack = new CharacterAttack(EnemyPrefab.CharacterEventAttackDetector, EnemyPrefab.Center, setDirection, EnemyPrefab.IgnoreLayer);
        _healthHandler = new HealthHandler(EnemyPrefab.HealthView, _enemyHealth, EnemyPrefab, _enemyStates);
        _detectorFinishAnimation = new DetectorFinishAnimation(_enemyAnimator);
        StateResetter stateResetter = new StateResetter(_detectorFinishAnimation, _enemyStates);
        
        Init();
    }

    private void Init()
    {
        _enemyStates.InitInitialState(States.Run);
        _enemyMovement.Init(moveSpeed: _enemyDataPreset.MoveSpeed);
        EnemyAttack.Init(distanceAttack: _enemyDataPreset.DistanceAttack, damage: _enemyDataPreset.InitialDamage);
        _healthHandler.Init(initialHealth: _enemyDataPreset.InitialHealth);
        
        _healthHandler.OnDie += SetDieState;
    }

    public void Activate()
    {
        EnemyPrefab.ActivateCollider();
        Reset();
        IsActive = true;
        EnemyPrefab.gameObject.SetActive(true);
        _enemyStates.Transit(States.Run);
    }
    
    private void SetDieState()
    {
        EnemyPrefab.DeactivateCollider();
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
        _healthHandler.Init(initialHealth: _enemyDataPreset.InitialHealth);
    }

    public void Deactivate()
    {
        IsActive = false;
        EnemyPrefab.gameObject.SetActive(false);
    }

    public void SetPosition(Vector2 spawnPosition) => EnemyPrefab.transform.position = spawnPosition;
}