using System;

public class DetectorFinishAnimation : IUpdateble
{
    private readonly HeroAnimator _heroAnimator;

    public event Action OnFinish;

    public DetectorFinishAnimation(HeroAnimator heroAnimator)
    {
        _heroAnimator = heroAnimator;
    }

    public void UpdateState(float dt)
    {
        if (_heroAnimator.IsAnimationFinish()) 
            OnFinish?.Invoke();
    }
}