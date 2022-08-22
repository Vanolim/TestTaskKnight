using UnityEngine;

public class StateResetter
{
    private readonly DetectorFinishAnimation _detectorFinishAnimation;
    private readonly IHeroStates _heroStates;

    public StateResetter(DetectorFinishAnimation detectorFinishAnimation, IHeroStates heroStates)
    {
        _detectorFinishAnimation = detectorFinishAnimation;
        _heroStates = heroStates;

        _detectorFinishAnimation.OnFinish += ResetState;
    }

    private void ResetState()
    {
        _heroStates.SetInitialState();
    }
}