using UnityEngine;

public class HeroMovement : IUpdateble
{
    private readonly Transform _hero;
    private readonly Rigidbody2D _rb;
    private readonly SpriteRenderer _spriteRenderer;
    private readonly ISetMoveDirection _setMoveDirection;
    
    private float _velocity;
    private States _currentStates;
    public Vector2 CurrentDirection { get; private set; }

    private const float MOVE_SPEED = 6;
    private const float BOUNCE_FORCE = 8.5f;
    private const float ROLL_SPEED = 10;
    private const float ROLL_DISTANCE = 0.5f;

    public HeroMovement(Transform hero, Rigidbody2D rb, IHeroStates heroStates, SpriteRenderer spriteRenderer,  ISetMoveDirection moveDirection)
    {
        _hero = hero;
        _rb = rb;
        _spriteRenderer = spriteRenderer;
        _setMoveDirection = moveDirection;

        heroStates.OnStateChanged += SetCurrentState;
    }
    

    public void UpdateState(float dt)
    {
        if(_currentStates == States.Run)
            Move(_setMoveDirection.MoveDirection, dt);
        
        if(_currentStates == States.Roll)
            Roll(dt);
    }

    private void SetCurrentState(States currentState)
    {
        _currentStates = currentState;
        
        if(_currentStates == States.Jump)
            Jump();
    }

    private void Move(Vector2 moveDirection, float dt)
    {
        CurrentDirection = moveDirection;
        FlipHeroToDirection();
        _hero.Translate((Vector3)moveDirection * (dt * MOVE_SPEED));
    }

    private void Jump() => _rb.velocity = Vector2.up * BOUNCE_FORCE;

    private void Roll(float dt) => 
        _hero.transform.position = Vector2.Lerp(_hero.transform.position, GetRollPosition(), ROLL_SPEED * dt);

    private Vector2 GetRollPosition() => 
        new Vector2(_hero.transform.position.x + CurrentDirection.x * ROLL_DISTANCE, _hero.transform.position.y);

    private void FlipHeroToDirection() => _spriteRenderer.flipX = CurrentDirection == Vector2.left;
}