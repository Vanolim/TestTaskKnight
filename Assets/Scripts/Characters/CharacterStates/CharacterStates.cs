using System;

public abstract class CharacterStates : ICharacterStates
{
    private States _initialStates = States.Idle;
    
    public event Action<States> OnStateChanged;
    public States CurrentCharacterState { get; private set; }

    public void SetInitialState() => SetState(_initialStates);

    public abstract void Transit(States transitCharacterState);

    protected void SetState(States characterState)
    {
        CurrentCharacterState = characterState;
        OnStateChanged?.Invoke(CurrentCharacterState);
    }

    public void InitInitialState(States states) => _initialStates = states;
}