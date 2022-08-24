using System;
using UnityEngine;

public class Hero : IUpdateble, IDisposable
{
    private readonly HeroPrefab _heroPrefab;
    private readonly IHealth _heroHealth;
    private readonly HeroMovement _heroMovement;
    private readonly CharacterAnimatorHandler _characterAnimatorHandler;
    private readonly CharacterAttack _heroAttack;
    private readonly HealthHandler _heroHealthHandler;
    private readonly HeroStatesHandler _heroStatesHandler;
    private CharacterAnimator _characterAnimator;
    private HeroDataPreset _heroDataPreset;
    
    public Transform Transform => _heroPrefab.transform;

    public event Action OnDead;

    public Hero(HeroPrefab heroPrefab, IHealth health, PlayerInput playerInput, HealthView healthView, HeroDataPreset heroDataPreset)
    {
        _heroDataPreset = heroDataPreset;
        _heroPrefab = heroPrefab;
        _heroHealth = health;
        
        _characterAnimator = new CharacterAnimator(_heroPrefab.Animator);
        _heroStatesHandler = new HeroStatesHandler(playerInput, _characterAnimator, _heroPrefab.RB);
        
        _heroMovement = new HeroMovement(_heroPrefab.transform, _heroStatesHandler.HeroStates, 
            _heroPrefab.SpriteRenderer, _heroStatesHandler.InputHandler, _heroPrefab.RB);
        
        _characterAnimatorHandler = new CharacterAnimatorHandler(_characterAnimator, 
            _heroStatesHandler.HeroStates, isUseRandomAttack: true);
        
        _heroAttack = new CharacterAttack(_heroPrefab.CharacterEventAttackDetector, _heroPrefab.Center, 
            _heroStatesHandler.InputHandler, _heroPrefab.IgnoreLayer);
        
        _heroHealthHandler = new HealthHandler(healthView, _heroHealth, _heroPrefab, _heroStatesHandler.HeroStates);

        Init();
    }

    private void Init()
    {
        _heroMovement.Init(moveSpeed: _heroDataPreset.MoveSpeed, bounceForce: _heroDataPreset.BounceForce, 
            rollSpeed: _heroDataPreset.RollDistance, rollDistance: _heroDataPreset.RollDistance);
        
        _heroAttack.Init(distanceAttack: _heroDataPreset.DistanceAttack, damage: _heroDataPreset.Damage);
        _heroHealthHandler.Init(initialHealth: _heroDataPreset.InitialHealth);
        
        _heroHealthHandler.OnDie += Die;
        _heroPrefab.OnTriggerAddHealth += AddHealth;
    }

    private void Die()
    {
        _heroStatesHandler.HeroStates.Transit(States.Die);
        OnDead?.Invoke();
    }

    private void AddHealth(IAddHealth addHealth) => _heroHealthHandler.AddHealth(addHealth);

    public void UpdateState(float dt)
    {
        _heroStatesHandler.UpdateState(dt);
        _heroMovement.UpdateState(dt);
    }

    public void Activate()
    {
        _heroPrefab.gameObject.SetActive(true);
        _heroStatesHandler.HeroStates.SetInitialState();
    }

    public void Dispose()
    {
        _heroStatesHandler.Dispose();
        _characterAnimatorHandler.Dispose();
        _heroAttack.Dispose();
        _heroHealthHandler.Dispose();
        _heroHealthHandler.OnDie -= Die;
        _heroPrefab.OnTriggerAddHealth -= AddHealth;
    }
}
