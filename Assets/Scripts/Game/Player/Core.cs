using UnityEngine;

public class Core : IDisposable
{
    private readonly CoreView _coreView;
    private readonly EnemyCollection _enemyCollection;
    private int _core;

    public Core(CoreView coreView, EnemyCollection enemyCollection)
    {
        _coreView = coreView;
        _enemyCollection = enemyCollection;

        InitView();
        _enemyCollection.OnEnemyKilled += AddCore;
    }

    private void InitView() => _coreView.UpdateCore(_core);

    private void AddCore(Enemy enemy)
    {
        _core += enemy.Worth;
        _coreView.UpdateCore(_core);
    }

    public void Dispose() => _enemyCollection.OnEnemyKilled -= AddCore;
}