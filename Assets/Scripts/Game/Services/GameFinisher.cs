using UnityEngine;

public class GameFinisher : IDisposable
{
    private readonly LoseView _loseView;
    private readonly StartView _startView;
    private readonly IDisposableHandler _disposableHandler;

    public GameFinisher(LoseView loseView, StartView startView, IDisposableHandler disposableHandler)
    {
        _loseView = loseView;
        _startView = startView;
        _disposableHandler = disposableHandler;

        loseView.OnExit += ExitGame;
        startView.OnExit += ExitGame;
    }

    private void ExitGame()
    {
        _disposableHandler.AllDispose();
        Application.Quit();
    }

    public void Dispose()
    {
        _loseView.OnExit += ExitGame;
        _startView.OnExit += ExitGame;
    }
}