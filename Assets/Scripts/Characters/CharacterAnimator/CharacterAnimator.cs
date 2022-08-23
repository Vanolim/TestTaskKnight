using UnityEngine;

public class CharacterAnimator : ICharacterAnimator
{
    private readonly Animator _animator;
    
    private readonly int Jump = Animator.StringToHash("Jump");
    private readonly int Attack1 = Animator.StringToHash("Attack1");
    private readonly int Attack2 = Animator.StringToHash("Attack2");
    private readonly int Attack3 = Animator.StringToHash("Attack3");
    private readonly int Fall = Animator.StringToHash("Fall");
    private readonly int GetDamage = Animator.StringToHash("GetDamage");
    private readonly int Roll = Animator.StringToHash("Roll");
    private readonly int Run = Animator.StringToHash("Run");
    private readonly int Idle = Animator.StringToHash("Idle");
    private readonly int Die = Animator.StringToHash("Die");

    private const string NAME_STATE_FINISH_ANIMATOR_DETECTOR = "FinishAnimatorDetector";

    public CharacterAnimator(Animator animator) => _animator = animator;

    public void SetIdle() => _animator.SetTrigger(Idle);

    public void SetRun() => _animator.SetTrigger(Run);
    
    public void SetJump() => _animator.SetTrigger(Jump);
    
    public void SetFall() => _animator.SetTrigger(Fall);

    public void SetAttack1() => _animator.SetTrigger(Attack1);
    
    public void SetAttack2() => _animator.SetTrigger(Attack2);
    
    public void SetAttack3() => _animator.SetTrigger(Attack3);

    public void SetGetDamage() => _animator.SetTrigger(GetDamage);

    public void SetDie(bool value) => _animator.SetBool(Die, value);

    public void SetRoll() => _animator.SetTrigger(Roll);

    public bool IsAnimationFinish() => _animator.GetCurrentAnimatorStateInfo(0).IsName(NAME_STATE_FINISH_ANIMATOR_DETECTOR);
}