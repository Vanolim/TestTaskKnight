using System;
using UnityEngine;

public class DetectorFinishAnimation : IDetectorFinishAnimation
{
    private readonly ICharacterAnimator _characterAnimator;
    public event Action OnFinish;

    public DetectorFinishAnimation(ICharacterAnimator characterAnimator)
    {
        _characterAnimator = characterAnimator;
    }

    public void UpdateState(float dt)
    {
        if (_characterAnimator.IsAnimationFinish())
        {
            OnFinish?.Invoke();
        }
    }
}