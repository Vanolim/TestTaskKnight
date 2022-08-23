using UnityEngine;

public class StateResetter
{
    private readonly IDetectorFinishAnimation _detectorFinishAnimation;
    private readonly ICharacterStates _characterStates;

    public StateResetter(IDetectorFinishAnimation detectorFinishAnimation, ICharacterStates characterStates)
    {
        _detectorFinishAnimation = detectorFinishAnimation;
        _characterStates = characterStates;

        _detectorFinishAnimation.OnFinish += ResetState;
    }

    private void ResetState() => _characterStates.SetInitialState();
}