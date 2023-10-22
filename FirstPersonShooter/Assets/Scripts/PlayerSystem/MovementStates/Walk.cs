using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MovementState
{
    protected const float defaultGravity= 2;

    protected float _speed;
    protected float _jumpSpeed;
    protected float _jumpVelocity;
    protected bool _isJump;

    

    public Walk(StateMachine stateMachine) : base(stateMachine)
    {
        _speed = _playerMovement.MovementSettings.WalkData.speed;
        _jumpSpeed = _playerMovement.MovementSettings.WalkData.jumpSpeed;
    }

    protected virtual void OnJumpPressed()
    {
        if (IsGrounded())
        {
            _jumpVelocity = _jumpSpeed;
        }
    }

    protected virtual void OnRunPressed()
    {
        _playerMovement.ChangeState(_playerMovement.States[MovementStates.Run]);
    }

    protected virtual void OnCrouchPressed()
    {
        _playerMovement.ChangeState(_playerMovement.States[MovementStates.Crouch]);
    }

    public override void CheckChangeState()
    {
        if (IsGrounded() == false)
        {
            _playerMovement.ChangeState(_playerMovement.States[MovementStates.Fall]);
        }
    }

    public override void OnEnter()
    {
        _playerMovement.MovementInput.JumpPressed += OnJumpPressed;
        _playerMovement.MovementInput.RunPressed += OnRunPressed;
        _playerMovement.MovementInput.CrouchPressed += OnCrouchPressed;
    }

    public override void OnExit()
    {
        _playerMovement.MovementInput.JumpPressed -= OnJumpPressed;
        _playerMovement.MovementInput.RunPressed -= OnRunPressed;
        _playerMovement.MovementInput.CrouchPressed -= OnCrouchPressed;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        UpdateMove();
    }

    protected virtual void UpdateMove()
    {
        Vector2 movementInput= _playerMovement.MovementInput.Move * _speed;
        Vector3 movementVector = _playerMovement.velocity;
        movementVector = _playerMovement.CharacterController.transform.forward * movementInput.y;
        movementVector += _playerMovement.CharacterController.transform.right * movementInput.x;
        movementVector = new Vector3(movementVector.x, _jumpVelocity - defaultGravity, movementVector.z);
        _jumpVelocity = 0;
        _playerMovement.velocity = movementVector;
        _playerMovement.CharacterController.Move(movementVector *  Time.deltaTime);
    }
}
