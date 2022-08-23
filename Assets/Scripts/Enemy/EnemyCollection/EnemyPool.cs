using System.Collections.Generic;
using System.Linq;

public class EnemyPool
{
    private readonly List<Enemy> _enemies = new List<Enemy>();

    public void AddEnemy(Enemy enemy) => _enemies.Add(enemy);

    public bool TryGetEnemy(out Enemy enemy)
    {
        enemy = _enemies.FirstOrDefault(x => x.IsActive == false);
        return enemy != null;
    }
}