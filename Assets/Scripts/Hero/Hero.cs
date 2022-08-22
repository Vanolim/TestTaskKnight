using UnityEngine;

public class Hero : IUpdateble
{
    private HeroPrefab _heroPrefab;
    private readonly HeroMovement _heroMovement;
    private readonly HeroAnimator _heroAnimator;
    private HeroAnimatorHandler _heroAnimatorHandler;
    private HeroAttack _heroAttack;
    private HeroHealthHandler _heroHealthHandler;
    private IHealth _heroHealth;

    public HeroAnimator HeroAnimator => _heroAnimator;

    public Hero(HeroPrefab heroPrefab, IHeroStates heroStates, ISetMoveDirection setMoveDirection, IHealth health)
    {
        _heroPrefab = heroPrefab;

        _heroMovement = new HeroMovement(_heroPrefab.transform, _heroPrefab.RB, heroStates,
            _heroPrefab.SpriteRenderer, setMoveDirection);

        _heroAnimator = new HeroAnimator(_heroPrefab.Animator);
        _heroAnimatorHandler = new HeroAnimatorHandler(_heroAnimator, heroStates);
        _heroAttack = new HeroAttack(heroStates, _heroPrefab.Center, _heroMovement, _heroPrefab.HeroLayer);
        _heroHealth = health;

        _heroHealthHandler = new HeroHealthHandler(FindView(), _heroHealth, _heroPrefab);
    }

    public void UpdateState(float dt)
    {
        _heroMovement.UpdateState(dt);
    }

    private HeroHealthView FindView() => Object.FindObjectOfType<HeroHealthView>();
}
