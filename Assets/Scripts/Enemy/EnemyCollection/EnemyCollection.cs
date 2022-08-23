using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollection : IUpdateble
{
    private readonly Transform _hero;
    private readonly EnemySpawner _enemySpawner;
    private readonly EnemyPool _enemyPool;
    private readonly EnemiesDamageEnhancer _enemiesDamageEnhancer;
    private int _countPoolEnemy = 2;

    private int _spawnTime = 2;
    private bool _isWork;
    private float _count;

    private readonly List<Enemy> _activeEnemies = new List<Enemy>();

    public event Action OnEnemyKilled;

    public EnemyCollection(Transform hero, EnemySpawner enemySpawner)
    {
        _hero = hero;
        _enemyPool = new EnemyPool();
        _enemiesDamageEnhancer = new EnemiesDamageEnhancer();
        _enemySpawner = enemySpawner;

        Init();
    }

    private void Init()
    {
        for (int i = 0; i < _countPoolEnemy; i++)
        {
            EnemyPrefab newEnemy = _enemySpawner.Spawn();
            _enemyPool.AddEnemy(EnemyInit(newEnemy));
        }
    }

    public void Start() => _isWork = true;

    private Enemy EnemyInit(EnemyPrefab enemyPrefab)
    {
        Enemy enemy = new Enemy(enemyPrefab, new EnemyStates(), new HeroPositionDetector(_hero, enemyPrefab.transform), new Health());
        _enemiesDamageEnhancer.AddEnemy(enemy);
        enemy.Deactivate();
        enemy.OnDie += DeactivateActiveEnemy;
        return enemy;
    }

    public void UpdateState(float dt)
    {
        if (_isWork)
        {
            _count += dt;
            if (_count >= _spawnTime)
            {
                GetNewActiveEnemy();
                _count = 0;
            }
        }

        for (int i = 0; i < _activeEnemies.Count; i++)
        {
            _activeEnemies[i].UpdateState(dt);
        }
    }

    private void GetNewActiveEnemy()
    {
        if (_enemyPool.TryGetEnemy(out Enemy enemy))
        {
            enemy.SetPosition(_enemySpawner.GetSpawnPosition());
            enemy.Activate();
            _activeEnemies.Add(enemy);
        }
    }

    private void DeactivateActiveEnemy(Enemy enemy)
    {
        OnEnemyKilled?.Invoke();
        _activeEnemies.Remove(enemy);
        enemy.Deactivate();
    }

    public void Deactivate()
    {
        _isWork = false;

        foreach (var activeEnemy in _activeEnemies)
        {
            activeEnemy.Deactivate();
        }
    }
}