public class Player
{
    private PlayerLose _playerLose;
    private Core _core;

    public Player(Hero hero, EnemyCollection enemyCollection, CoreView coreView)
    {
        _playerLose = new PlayerLose(hero);
        _core = new Core(coreView, enemyCollection);
    }
}