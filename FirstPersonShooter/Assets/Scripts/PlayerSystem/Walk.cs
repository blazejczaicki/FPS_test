using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MovementState
{
    public Walk(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void CheckChangeState()
    {
    }

    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        UpdateMove();
    }

    protected virtual void UpdateMove()
    {
        Vector2 movementInput= _playerMovement.MovementInput.Move * _playerMovement.MovementSettings.WalkData.speed;
        Vector3 movementVector = Vector3.zero;
        movementVector += _playerMovement.CharacterController.transform.forward * movementInput.y;
        movementVector += _playerMovement.CharacterController.transform.right * movementInput.x;
        _playerMovement.CharacterController.Move(movementVector *  Time.deltaTime);
    }
}
