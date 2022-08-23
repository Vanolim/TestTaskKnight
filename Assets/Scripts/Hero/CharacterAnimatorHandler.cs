using UnityEngine;

public class CharacterAnimatorHandler
{
    private readonly CharacterAnimator _characterAnimator;
    private ICharacterStates _characterStates;

    public CharacterAnimatorHandler(CharacterAnimator characterAnimator, ICharacterStates characterStates)
    {
        _characterAnimator = characterAnimator;
        _characterStates = characterStates;

        _characterStates.OnStateChanged += SetAnimation;
    }
    
    public void Reset() =>  _characterAnimator.SetDie(false);

    private void SetAnimation(States currentCharacterState)
    {
        switch (currentCharacterState)
        {
            case States.Idle:
                _characterAnimator.SetIdle();
                break;
            case States.Run:
                _characterAnimator.SetRun();
                break;
            case States.Jump:
                _characterAnimator.SetJump();
                break;
            case States.Fall:
                _characterAnimator.SetFall();
                break;
            case States.Roll:
                _characterAnimator.SetRoll();
                break;
            case States.Attack:
                //SetRandomAnimationAttack();
                _characterAnimator.SetAttack1();
                break;
            case States.GetDamage:
                _characterAnimator.SetGetDamage();
                break;
            case States.Die:
                _characterAnimator.SetDie(true);
                break;
        }
    }

    private void SetRandomAnimationAttack()
    {
        int numberAttackAnimations = 3;

        switch (Random.Range(0, numberAttackAnimations))
        {
            case 0:
                _characterAnimator.SetAttack1();
                break;
            case 1:
                _characterAnimator.SetAttack2();
                break;
            case 2:
                _characterAnimator.SetAttack3();
                break;
        }
    }
}