using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : Walk
{
    public Crouch(StateMachine stateMachine) : base(stateMachine)
    {
        _speed = _playerMovement.MovementSettings.CrouchData.speed;
    }

    public override void CheckChangeState()
    {
        if (IsGrounded() == false)
        {
            _playerMovement.ChangeState(_playerMovement.States[MovementStates.Fall]);
        }

        if (IsGrounded() == false)
        {
            _playerMovement.ChangeState(_playerMovement.States[MovementStates.Walk]);
        }

        if (IsGrounded() == false)
        {
            _playerMovement.ChangeState(_playerMovement.States[MovementStates.Run]);
        }
    }
}
