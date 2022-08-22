using UnityEngine;

public class Hero : IUpdateble
{
    private readonly HeroPrefab _heroPrefab;
    private readonly IHealth _heroHealth;
    private HeroMovement _heroMovement;
    private CharacterAnimator _characterAnimator;
    private CharacterAnimatorHandler _characterAnimatorHandler;
    private CharacterAttack _heroAttack;
    private HealthHandler _heroHealthHandler;

    public CharacterAnimator CharacterAnimator => _characterAnimator;

    public Hero(HeroPrefab heroPrefab, ICharacterStates heroStates, ISetDirection setDirection, IHealth health)
    {
        _heroPrefab = heroPrefab;
        _heroHealth = health;

        Init(heroStates, setDirection);
    }

    private void Init(ICharacterStates heroStates, ISetDirection setDirection)
    {
        _heroMovement = new HeroMovement(_heroPrefab.transform, heroStates, _heroPrefab.SpriteRenderer, setDirection,
            _heroPrefab.RB);
        _heroMovement.Init(moveSpeed: 6, bounceForce: 8.5f, rollSpeed: 10, rollDistance: 0.5f);

        _characterAnimator = new CharacterAnimator(_heroPrefab.Animator);
        _characterAnimatorHandler = new CharacterAnimatorHandler(_characterAnimator, heroStates);
        _heroAttack = new CharacterAttack(heroStates, _heroPrefab.Center, setDirection, _heroPrefab.HeroLayer);

        _heroHealthHandler = new HealthHandler(FindView(), _heroHealth, _heroPrefab);
        _heroHealthHandler.Init(initialHealth: 20);
    }

    public void UpdateState(float dt) => _heroMovement.UpdateState(dt);

    private IHealthView FindView() => Object.FindObjectOfType<HealthView>();
}
