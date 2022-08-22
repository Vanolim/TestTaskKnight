using System.Collections.Generic;
using System.Linq;

public class EnemyPool
{
    private List<Enemy> _enemies = new List<Enemy>();

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    public Enemy GetEnemy() => _enemies.First(x => x.IsActive == false);
}