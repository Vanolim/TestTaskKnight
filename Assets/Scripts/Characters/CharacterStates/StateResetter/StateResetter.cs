public class StateResetter : IDisposable
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
    public void Dispose() => _detectorFinishAnimation.OnFinish -= ResetState;
}