public struct HeroDataPreset
{
    public float MoveSpeed => MOVE_SPPED;
    public float BounceForce => BOUNCE_FORCE;
    public float RollSpeed => ROLL_SPEED;
    public float RollDistance => ROLL_DISTANCE;
    public float DistanceAttack => DISTANCE_ATTACK;
    public float Damage => DAMAGE;
    public int InitialHealth => INITIAL_HEALTH;

    private const float MOVE_SPPED = 7;
    private const float BOUNCE_FORCE = 8.5f;
    private const float ROLL_SPEED = 15;
    private const float ROLL_DISTANCE = 3f;
    private const float DISTANCE_ATTACK = 3;
    private const float DAMAGE = 1;
    private const int INITIAL_HEALTH = 20;
}