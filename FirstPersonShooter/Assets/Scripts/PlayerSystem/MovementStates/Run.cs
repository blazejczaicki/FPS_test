using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : Walk
{
    public Run(StateMachine stateMachine) : base(stateMachine)
    {
        _speed = _playerMovement.MovementSettings.RunData.speed;
    }

    public override void OnEnter()
    {
        _playerMovement.MovementInput.JumpPressed += OnJumpPressed;
        _playerMovement.MovementInput.RunReleased += OnRunReleased;
    }

    public override void OnExit()
    {
        _playerMovement.MovementInput.JumpPressed -= OnJumpPressed;
        _playerMovement.MovementInput.RunReleased -= OnRunReleased;
    }

    protected virtual void OnRunReleased()
    {
        _playerMovement.ChangeState(_playerMovement.States[MovementStates.Walk]);
    }
}
