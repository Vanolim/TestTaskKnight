using System.Collections.Generic;

public class EnemiesDamageEnhancer : IUpdateble
{
    private List<Enemy> _enemies = new List<Enemy>();
    private float _counter;

    private const float DAMAGE_UPGRADE_TIME = 10f;

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
    }
    
    public void UpdateState(float dt)
    {
        _counter += dt;

        if (_counter >= DAMAGE_UPGRADE_TIME)
        {
            UpgradeEnemies();

            _counter = 0;
        }
    }

    private void UpgradeEnemies()
    {
        foreach (var enemy in _enemies)
        {
            enemy.UpgradeDamage();
        }
    }
}