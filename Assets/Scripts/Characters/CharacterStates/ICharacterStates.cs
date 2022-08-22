using System;

public interface ICharacterStates
{
    public event Action<States> OnStateChanged;
    public States CurrentCharacterState { get; }
    public void SetInitialState();
    public void Transit(States transitCharacterState);
}