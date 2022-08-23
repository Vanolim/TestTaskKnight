public class Player
{
    public PlayerLose PlayerLose { get; }
    public Core Core { get; }

    public Player(Hero hero, EnemyCollection enemyCollection, CoreView coreView, LoseView loseView)
    {
        PlayerLose = new PlayerLose(hero, loseView);
        Core = new Core(coreView, enemyCollection);
    }
}