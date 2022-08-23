using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class Hero : IUpdateble
{
    private readonly HeroPrefab _heroPrefab;
    private readonly IHealth _heroHealth;
    private HeroMovement _heroMovement;
    private CharacterAnimator _characterAnimator;
    private CharacterAnimatorHandler _characterAnimatorHandler;
    private CharacterAttack _heroAttack;
    private HealthHandler _heroHealthHandler;
    private HeroStatesHandler _heroStatesHandler;

    public CharacterAnimator CharacterAnimator => _characterAnimator;
    public Transform Transform => _heroPrefab.transform;

    public event Action OnDead;

    public Hero(HeroPrefab heroPrefab, IHealth health, PlayerInput playerInput)
    {
        _heroPrefab = heroPrefab;
        _heroHealth = health;

        Init(playerInput);
    }

    private void Init(PlayerInput playerInput)
    {
        _characterAnimator = new CharacterAnimator(_heroPrefab.Animator);
        
        _heroStatesHandler = new HeroStatesHandler(playerInput, _characterAnimator, _heroPrefab.RB);

        _heroMovement = new HeroMovement(_heroPrefab.transform, _heroStatesHandler.HeroStates, 
            _heroPrefab.SpriteRenderer, _heroStatesHandler.InputHandler, _heroPrefab.RB);
        _heroMovement.Init(moveSpeed: 6, bounceForce: 8.5f, rollSpeed: 10, rollDistance: 0.5f);

        _characterAnimatorHandler = new CharacterAnimatorHandler(_characterAnimator, _heroStatesHandler.HeroStates);
        
        _heroAttack = new CharacterAttack(_heroPrefab.CharacterEventAttackDetector, _heroPrefab.Center, 
            _heroStatesHandler.InputHandler, _heroPrefab.HeroLayer);
        _heroAttack.Init(distanceAttack: 3, damage: 1);

        _heroHealthHandler = new HealthHandler(FindView(), _heroHealth, _heroPrefab, _heroStatesHandler.HeroStates);
        _heroHealthHandler.Init(initialHealth: 20);
        _heroHealthHandler.OnDie += Die;
    }

    private void Die()
    {
        _heroStatesHandler.HeroStates.Transit(States.Die);
        OnDead?.Invoke();
    }

    public void UpdateState(float dt)
    {
        _heroStatesHandler.UpdateState(dt);
        _heroMovement.UpdateState(dt);
    }

    private IHealthView FindView() => Object.FindObjectOfType<HealthView>();
}
