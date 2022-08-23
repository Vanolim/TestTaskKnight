using UnityEngine;

public class GameFinisher : IDisposable
{
    private readonly LoseView _loseView;
    private readonly StartView _startView;
    public GameFinisher(LoseView loseView, StartView startView)
    {
        _loseView = loseView;
        _startView = startView;
        
        loseView.OnExit += ExitGame;
        startView.OnExit += ExitGame;
    }

    private void ExitGame() => Application.Quit();
    public void Dispose()
    {
        _loseView.OnExit += ExitGame;
        _startView.OnExit += ExitGame;
    }
}