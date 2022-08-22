using UnityEngine;

public class HeroPositionDetector : ISetDirection
{
    private readonly Transform _hero;
    public Vector2 MoveDirection => _hero.position;

    public HeroPositionDetector(Transform hero)
    {
        _hero = hero;
    }
}