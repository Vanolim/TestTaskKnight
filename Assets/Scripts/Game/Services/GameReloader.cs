﻿using UnityEngine.SceneManagement;

public class GameReloader : IDisposable
{
    private LoseView _loseView;
    public GameReloader(LoseView loseView)
    {
        _loseView = loseView;
        loseView.OnRestart += Reload;
    }
    
    public void Reload() => SceneManager.LoadScene("Game");
    public void Dispose() => _loseView.OnRestart -= Reload;
}