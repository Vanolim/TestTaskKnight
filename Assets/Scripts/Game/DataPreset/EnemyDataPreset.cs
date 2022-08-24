public struct EnemyDataPreset
{
    public float MoveSpeed => MOVE_SPPED;
    public float DistanceAttack => DISTANCE_ATTACK;
    public int Worth => WORTH;
    public float InitialDamage => INITIAL_DAMAGE;
    public int InitialHealth => INITIAL_HEALTH;

    private const float MOVE_SPPED = 6;
    private const float DISTANCE_ATTACK = 1.8f;
    private const float INITIAL_DAMAGE = 1;
    private const int INITIAL_HEALTH = 1;
    private const int WORTH = 1;
}