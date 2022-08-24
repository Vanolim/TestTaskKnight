using UnityEngine;

public class HeroStatesHandler : IUpdateble, IDisposable
{
    private readonly IDetectorFinishAnimation _detectorFinishAnimation;
    private readonly StateResetter _stateResetter;
    private readonly HeroBoundHandler _heroBoundHandler;
    public ICharacterStates HeroStates { get; }
    public InputHandler InputHandler { get; }

    public HeroStatesHandler(PlayerInput playerInput, ICharacterAnimator characterAnimator, Rigidbody2D rb)
    {
        HeroStates = new HeroStates();
        InputHandler = new InputHandler(playerInput, HeroStates);
        _detectorFinishAnimation = new DetectorFinishAnimation(characterAnimator);
        _stateResetter = new StateResetter(_detectorFinishAnimation, HeroStates);
        _heroBoundHandler = new HeroBoundHandler(InputHandler, rb, HeroStates);
    }

    public void UpdateState(float dt)
    {
        _detectorFinishAnimation.UpdateState(dt);
        _heroBoundHandler.UpdateState(dt);
    }

    public void Dispose()
    {
        _stateResetter.Dispose();
        _heroBoundHandler.Dispose();
    }
}