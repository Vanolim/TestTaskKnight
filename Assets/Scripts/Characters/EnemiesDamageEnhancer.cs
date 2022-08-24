using System.Collections.Generic;
using UnityEngine;

public class EnemiesDamageEnhancer : IUpdateble
{
    private readonly IReadOnlyList<Enemy> _enemies;
    private float _counter;
    private EnemyUpgradeDataPreset _enemyUpgradeDataPreset;

    public EnemiesDamageEnhancer(IReadOnlyList<Enemy> enemies, EnemyUpgradeDataPreset enemyUpgradeDataPreset)
    {
        _enemies = enemies;
        _enemyUpgradeDataPreset = enemyUpgradeDataPreset;
    }

    public void UpdateState(float dt)
    {
        _counter += dt;

        if (_counter >= _enemyUpgradeDataPreset.DamageUpgradeTime)
        {
            UpgradeEnemies();
            _counter = 0;
        }
    }

    private void UpgradeEnemies()
    {
        foreach (var enemy in _enemies)
        {
            enemy.EnemyAttack.UpgradeDamage(_enemyUpgradeDataPreset.DamageUpgradeValue);
        }
    }
}