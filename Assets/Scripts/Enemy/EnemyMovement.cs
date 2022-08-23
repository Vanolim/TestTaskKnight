using UnityEngine;

public class EnemyMovement : CharacterMovement
{
    public EnemyMovement(Transform character, ICharacterStates characterStates, SpriteRenderer spriteRenderer, ISetDirection setDirection) 
        : base(character, characterStates, spriteRenderer, setDirection)
    {
        
    }

    public void Init(float moveSpeed) => SetMoveSpeed(moveSpeed);

    public override void UpdateState(float dt)
    {
        if (CurrentCharacterStates == States.Run) 
            Move(dt);
    }

    protected override void SetCurrentState(States currentCharacterState) => CurrentCharacterStates = currentCharacterState;
}