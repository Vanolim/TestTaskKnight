using UnityEngine;

public class EnemyCollection
{
    private readonly Transform _hero;
    private EnemySpawner _enemySpawner;
    private readonly EnemyPool _enemyPool;
    private int _countPoolEnemy = 8;

    public EnemyCollection(Transform hero)
    {
        _hero = hero;
        _enemyPool = new EnemyPool();
    }

    public void Init()
    {
        FindEnemySpawner();
        for (int i = 0; i < _countPoolEnemy; i++)
        {
            EnemyPrefab newEnemy = _enemySpawner.Spawn();
            
            _enemyPool.AddEnemy(EnemyInit(newEnemy));
        }

        Enemy enemy = _enemyPool.GetEnemy();
        enemy.Activate();
    }

    private Enemy EnemyInit(EnemyPrefab enemyPrefab)
    {
        Enemy enemy = new Enemy(enemyPrefab, new HeroStates(), new HeroPositionDetector(_hero), new Health());
        enemy.Deactivate();
        return enemy;
    }

    private void FindEnemySpawner()
    {
        _enemySpawner = Object.FindObjectOfType<EnemySpawner>();
    }
}