using UnityEngine;

public abstract class CharacterMovement : IUpdateble
{
    private readonly Transform _character;
    private readonly SpriteRenderer _spriteRenderer;
    private readonly ISetDirection _setDirection;
    private readonly ICharacterStates _characterStates;

    private float _moveSpeed;
    
    protected States CurrentCharacterStates;
    protected Vector2 Direction => _setDirection.MoveDirection;
    protected Transform Character => _character;

    public CharacterMovement(Transform character, ICharacterStates characterStates, 
        SpriteRenderer spriteRenderer, ISetDirection setDirection)
    {
        _character = character;
        _characterStates = characterStates;
        _spriteRenderer = spriteRenderer;
        _setDirection = setDirection;
        
        _characterStates.OnStateChanged += SetCurrentState;
    }

    public void SetMoveSpeed(float value) => _moveSpeed = value;

    public abstract void UpdateState(float dt);
    
    protected abstract void SetCurrentState(States currentCharacterState);

    protected void Move(float dt)
    {
        FlipHeroToDirection(Direction);
        _character.Translate((Vector3)Direction * (dt * _moveSpeed));
    }
    
    private void FlipHeroToDirection(Vector2 moveDirection) => _spriteRenderer.flipX = moveDirection == Vector2.left;
}