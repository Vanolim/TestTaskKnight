using UnityEngine;

public class HeroBoundHandler : IUpdateble
{
    private readonly InputHandler _inputHandler;
    private readonly ICharacterStates _heroStates;
    private readonly Rigidbody2D _heroRB;

    private bool _isFallSet;
    private bool _isGroundSet;
    private bool _isJump;

    private const float FALL_RATE = -1f;

    public HeroBoundHandler(InputHandler inputHandler, Rigidbody2D heroRB, ICharacterStates heroStates)
    {
        _inputHandler = inputHandler;
        _heroStates = heroStates;
        _heroRB = heroRB;

        _inputHandler.OnInputJump += TryJump;
    }

    private void TryJump()
    {
        if (_heroRB.velocity == Vector2.zero && _heroStates.CurrentCharacterState is States.Idle or States.Run)
        {
            _heroStates.Transit(States.Jump);
            SetJump();
        }
    }

    public void UpdateState(float dt)
    {
        if (_isJump)
        {
            if (IsFall()) 
                SetStateFall();

            if (IsFinishedFalling()) 
                SetOnGrounded();
        }
        else
        {
            CheckFall();
        }
    }

    private void CheckFall()
    {
        if (_heroRB.velocity.y < FALL_RATE) 
            SetJump();
    }

    private void SetJump()
    {
        _isJump = true;
        _isGroundSet = false;
        _isFallSet = false;
    }

    private void SetStateFall()
    {
        _heroStates.Transit(States.Fall);
        _isFallSet = true;
    }

    private void SetOnGrounded()
    {
        _heroStates.SetInitialState();
        _isGroundSet = false;
        _isJump = false;
    }

    private bool IsFall() => _isFallSet == false && _heroRB.velocity.y < FALL_RATE;
    private bool IsFinishedFalling() => _isGroundSet == false && _isFallSet && _heroRB.velocity.y == 0;
}