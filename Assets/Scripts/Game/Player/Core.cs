public class Core : IDisposable
{
    private readonly CoreView _coreView;
    private readonly EnemyCollection _enemyCollection;
    private int _core = 0;

    private const int CORE_FOR_KILLED_ENEMY = 1;
    
    public Core(CoreView coreView, EnemyCollection enemyCollection)
    {
        _coreView = coreView;
        _enemyCollection = enemyCollection;

        InitView();
        _enemyCollection.OnEnemyKilled += AddCore;
    }

    private void InitView() => _coreView.UpdateCore(_core);

    private void AddCore()
    {
        _core += CORE_FOR_KILLED_ENEMY;
        _coreView.UpdateCore(_core);
    }

    public void Dispose() => _enemyCollection.OnEnemyKilled -= AddCore;
}