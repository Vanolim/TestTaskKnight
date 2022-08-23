using UnityEngine;

public class HeroPositionDetector : ISetDirection
{
    private readonly Transform _hero;
    private readonly Transform _enemy;
    public Vector2 Direction => new(_hero.position.x - _enemy.position.x, 0);

    public HeroPositionDetector(Transform hero, Transform enemy)
    {
        _hero = hero;
        _enemy = enemy;
    }
}