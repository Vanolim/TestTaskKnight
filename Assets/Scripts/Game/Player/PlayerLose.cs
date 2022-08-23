using System;

public class PlayerLose : IDisposable
{
    private readonly Hero _hero;
    private readonly LoseView _loseView;

    public event Action OnLose;
      
    public PlayerLose(Hero hero, LoseView loseView)
    {
        _loseView = loseView;
        _hero = hero;
        _hero.OnDead += Lose;
    }

    private void Lose()
    {
        _loseView.Activate();
        OnLose?.Invoke();
    }

    public void Dispose() => _hero.OnDead -= Lose;
}