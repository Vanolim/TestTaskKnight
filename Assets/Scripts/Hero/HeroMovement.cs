using UnityEngine;

public class HeroMovement : CharacterMovement
{
    private readonly Rigidbody2D _rb;

    private float _bounceForce;
    private float _rollSpeed;
    private float _rollDistance;

    public HeroMovement(Transform character, ICharacterStates characterStates, SpriteRenderer spriteRenderer, 
        ISetDirection setDirection, Rigidbody2D rb) : base(character, characterStates, spriteRenderer, setDirection)
    {
        _rb = rb;
    }

    public void Init(float moveSpeed, float bounceForce, float rollSpeed, float rollDistance)
    {
        SetMoveSpeed(moveSpeed);
        _bounceForce = bounceForce;
        _rollSpeed = rollSpeed;
        _rollDistance = rollDistance;
    }

    public override void UpdateState(float dt)
    {
        if(CurrentCharacterStates == States.Run)
            Move(dt);
        
        if(CurrentCharacterStates == States.Roll)
            Roll(dt);
    }

    protected override void SetCurrentState(States currentCharacterState)
    {
        CurrentCharacterStates = currentCharacterState;
        
        if(CurrentCharacterStates == States.Jump)
            Jump();
    }

    private void Jump() => _rb.velocity = Vector2.up * _bounceForce;

    private void Roll(float dt) => 
        Character.transform.position = Vector2.Lerp(Character.transform.position, GetRollPosition(), _rollSpeed * dt);

    private Vector2 GetRollPosition() => 
        new(Character.transform.position.x + Direction.x * _rollDistance, Character.transform.position.y);
}