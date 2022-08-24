using UnityEngine;

public abstract class CharacterMovement : IUpdateble
{
    private readonly Transform _character;
    private readonly SpriteRenderer _spriteRenderer;
    private readonly ISetDirection _setDirection;

    private float _moveSpeed;
    
    protected States CurrentCharacterStates;
    protected Vector2 Direction => _setDirection.Direction.normalized;
    protected Transform Character => _character;

    public CharacterMovement(Transform character, ICharacterStates characterStates, 
        SpriteRenderer spriteRenderer, ISetDirection setDirection)
    {
        _character = character;
        _spriteRenderer = spriteRenderer;
        _setDirection = setDirection;
        
        characterStates.OnStateChanged += SetCurrentState;
    }

    protected void SetMoveSpeed(float value) => _moveSpeed = value;

    public abstract void UpdateState(float dt);
    
    protected abstract void SetCurrentState(States currentCharacterState);

    protected void Move(float dt)
    {
        FlipHeroToDirection(Direction);
        _character.Translate((Vector3)Direction * (dt * _moveSpeed));
    }
    
    private void FlipHeroToDirection(Vector2 moveDirection) => _spriteRenderer.flipX = moveDirection == Vector2.left;
}