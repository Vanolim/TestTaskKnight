using System;
using UnityEngine;

public class InputHandler : ISetDirection
{
    private readonly ICharacterStates _heroStates;
    private readonly PlayerInput _playerInput;

    public Vector2 MoveDirection => _playerInput.Hero.Move.ReadValue<Vector2>();

    public event Action OnInputJump;

    public InputHandler(PlayerInput playerInput, ICharacterStates heroStates)
    {
        _playerInput = playerInput;
        _heroStates = heroStates;

        SubscribeInput();
    }
    
    private void SubscribeInput()
    {
        _playerInput.Hero.Move.started += _ => OnMove();
        _playerInput.Hero.Move.canceled += _ => OnStopMove();
        _playerInput.Hero.Bounce.started += _ => OnJump();
        _playerInput.Hero.Attack.started += _ => OnAttack();
        _playerInput.Hero.Roll.started += _ => OnRoll();
    }

    private void OnMove()
    {
        _heroStates.Transit(States.Run);
    }

    private void OnStopMove()
    {
        _heroStates.Transit(States.Idle);
    }

    private void OnJump()
    {
        OnInputJump?.Invoke();
    }

    private void OnAttack()
    {
        _heroStates.Transit(States.Attack);
    }

    private void OnRoll()
    {
        _heroStates.Transit(States.Roll);
    }
}