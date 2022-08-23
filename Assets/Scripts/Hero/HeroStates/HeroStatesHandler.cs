using UnityEngine;

public class HeroStatesHandler : IUpdateble
{
    private readonly IDetectorFinishAnimation _detectorFinishAnimation;
    private StateResetter _stateResetter;
    private HeroBoundHandler _heroBoundHandler;
    public ICharacterStates HeroStates { get; }
    public InputHandler InputHandler { get; }

    public HeroStatesHandler(PlayerInput playerInput, ICharacterAnimator characterAnimator, Rigidbody2D rb)
    {
        HeroStates = new HeroStates();
        InputHandler = new InputHandler(playerInput, HeroStates);
        _detectorFinishAnimation = new DetectorFinishAnimation(characterAnimator);
        _stateResetter = new StateResetter(_detectorFinishAnimation, HeroStates);
        _heroBoundHandler = new HeroBoundHandler(InputHandler, rb, HeroStates);
        
        HeroStates.SetInitialState();
    }

    public void UpdateState(float dt)
    {
        _detectorFinishAnimation.UpdateState(dt);
    }
}