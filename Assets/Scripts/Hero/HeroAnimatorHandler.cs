using UnityEngine;

public class HeroAnimatorHandler
{
    private readonly HeroAnimator _heroAnimator;
    private IHeroStates _heroStates;

    public HeroAnimatorHandler(HeroAnimator heroAnimator, IHeroStates heroStates)
    {
        _heroAnimator = heroAnimator;
        _heroStates = heroStates;

        _heroStates.OnStateChanged += SetAnimation;
    }

    private void SetAnimation(States currentState)
    {
        switch (currentState)
        {
            case States.Idle:
                _heroAnimator.SetIdle();
                break;
            case States.Run:
                _heroAnimator.SetRun();
                break;
            case States.Jump:
                _heroAnimator.SetJump();
                break;
            case States.Fall:
                _heroAnimator.SetFall();
                break;
            case States.Roll:
                _heroAnimator.SetRoll();
                break;
            case States.Attack:
                SetRandomAnimationAttack();
                break;
            case States.GetDamage:
                _heroAnimator.SetGetDamage();
                break;
            case States.Die:
                _heroAnimator.SetDie();
                break;
        }
    }

    private void SetRandomAnimationAttack()
    {
        int numberAttackAnimations = 3;

        switch (Random.Range(0, numberAttackAnimations))
        {
            case 0:
                _heroAnimator.SetAttack1();
                break;
            case 1:
                _heroAnimator.SetAttack2();
                break;
            case 2:
                _heroAnimator.SetAttack3();
                break;
        }
    }
}