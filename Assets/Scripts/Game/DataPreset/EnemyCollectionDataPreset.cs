public struct EnemyCollectionDataPreset
{
    public EnemyDataPreset EnemyDataPreset { get; }
    public EnemyUpgradeDataPreset EnemyUpgradeDataPreset { get; }
    public int CountPollEnemy => COUNT_POOL_ENEMY;
    public float TimeSpawnNewEnemy => SPAWN_TIME_NEW_ENEMY;

    private const int COUNT_POOL_ENEMY = 6;
    private const float SPAWN_TIME_NEW_ENEMY = 3f;
}