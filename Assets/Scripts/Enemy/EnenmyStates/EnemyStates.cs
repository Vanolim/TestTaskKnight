public class EnemyStates : CharacterStates
{
    public override void Transit(States transitCharacterState) => SetState(transitCharacterState);
}