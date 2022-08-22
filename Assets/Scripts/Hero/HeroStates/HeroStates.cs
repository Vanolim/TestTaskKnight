using System;

public class HeroStates : IHeroStates
{
    public event Action<States> OnStateChanged;
    public States CurrentState { get; private set; }

    public void SetInitialState() => SetState(States.Idle);

    public void Transit(States transitState)
    {
        if (CheckPossibleTransition(transitState))
            SetState(transitState);
    }

    private void SetState(States state)
    {
        CurrentState = state;
        OnStateChanged?.Invoke(CurrentState);
    }

    private bool CheckPossibleTransition(States transitState)
    {
        if (CurrentState == States.Jump)
        {
            if (transitState == States.Fall)
                return true;
        }

        if (CurrentState is States.Idle or States.Run)
        {
            return true;
        }
        
        return false;
    }
}