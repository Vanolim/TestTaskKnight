public class HeroStates : CharacterStates
{
    public override void Transit(States transitCharacterState)
    {
        if (CheckPossibleTransition(transitCharacterState))
            SetState(transitCharacterState);
    }
    
    private bool CheckPossibleTransition(States transitCharacterState)
    {
        if (CurrentCharacterState == States.Jump)
        {
            if (transitCharacterState == States.Fall)
                return true;
        }

        if (CurrentCharacterState is States.Idle or States.Run or States.GetDamage)
        {
            return true;
        }
        

        return false;
    }
}