using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollection : IUpdateble
{
    private readonly Transform _hero;
    private readonly EnemySpawner _enemySpawner;
    private readonly EnemyPool _enemyPool;
    private readonly EnemiesDamageEnhancer _enemiesDamageEnhancer;
    private EnemyCollectionDataPreset _enemyCollectionDataPreset;
    
    private bool _isWork;
    private float _count;

    private List<Enemy> _activeEnemies = new List<Enemy>();

    public event Action<Enemy> OnEnemyKilled;

    public EnemyCollection(Transform hero, EnemySpawner enemySpawner, EnemyCollectionDataPreset enemyCollectionDataPreset)
    {
        _enemyCollectionDataPreset = enemyCollectionDataPreset;
        _hero = hero;
        _enemyPool = new EnemyPool();
        _enemySpawner = enemySpawner;
        InitCollection();

        _enemiesDamageEnhancer = new EnemiesDamageEnhancer(_enemyPool.Enemies, _enemyCollectionDataPreset.EnemyUpgradeDataPreset);
    }

    private void InitCollection()
    {
        for (int i = 0; i < _enemyCollectionDataPreset.CountPollEnemy; i++)
        {
            EnemyPrefab newEnemy = _enemySpawner.Spawn();
            _enemyPool.AddEnemy(EnemyInit(newEnemy));
        }
    }

    public void Start() => _isWork = true;

    private Enemy EnemyInit(EnemyPrefab enemyPrefab)
    {
        Enemy enemy = new Enemy(enemyPrefab, new EnemyStates(), 
            new HeroPositionDetector(_hero, enemyPrefab.transform), new Health(), _enemyCollectionDataPreset.EnemyDataPreset);
        
        enemy.Deactivate();
        enemy.OnDie += DeactivateActiveEnemy;
        return enemy;
    }

    public void UpdateState(float dt)
    {
        if (_isWork)
        {
            _count += dt;
            if (_count >= _enemyCollectionDataPreset.TimeSpawnNewEnemy)
            {
                if(GetNewActiveEnemy())
                    _count = 0;
            }

            for (int i = 0; i < _activeEnemies.Count; i++)
            {
                _activeEnemies[i].UpdateState(dt);
            }

            _enemiesDamageEnhancer.UpdateState(dt);
        }
    }

    private bool GetNewActiveEnemy()
    {
        if (_enemyPool.TryGetEnemy(out Enemy enemy))
        {
            enemy.SetPosition(_enemySpawner.GetSpawnPosition());
            enemy.Activate();
            _activeEnemies.Add(enemy);
            return true;
        }

        return false;
    }

    private void DeactivateActiveEnemy(Enemy enemy)
    {
        OnEnemyKilled?.Invoke(enemy);
        _activeEnemies.Remove(enemy);
        enemy.Deactivate();
    }

    public void Deactivate()
    {
        _isWork = false;
        
        for (int i = 0; i < _activeEnemies.Count; i++)
        {
            _activeEnemies[i].Deactivate();
        }

        _activeEnemies = new List<Enemy>();
    }
}