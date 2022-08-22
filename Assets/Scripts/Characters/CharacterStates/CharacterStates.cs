using System;

public abstract class CharacterStates : ICharacterStates
{
    public event Action<States> OnStateChanged;
    public States CurrentCharacterState { get; private set; }

    public void SetInitialState() => SetState(States.Idle);

    public abstract void Transit(States transitCharacterState);

    protected void SetState(States characterState)
    {
        CurrentCharacterState = characterState;
        OnStateChanged?.Invoke(CurrentCharacterState);
    }

    private bool CheckPossibleTransition(States transitCharacterState)
    {
        if (CurrentCharacterState == States.Jump)
        {
            if (transitCharacterState == States.Fall)
                return true;
        }

        if (CurrentCharacterState is States.Idle or States.Run)
        {
            return true;
        }
        
        return false;
    }
}