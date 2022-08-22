using System;

public interface IHeroStates
{
    public event Action<States> OnStateChanged;
    public States CurrentState { get; }
    public void SetInitialState();
    public void Transit(States transitState);
}