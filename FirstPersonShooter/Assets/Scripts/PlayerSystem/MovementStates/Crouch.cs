using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : Walk
{
    protected float normalHeight;
    protected float crouchHeight;

    public Crouch(StateMachine stateMachine) : base(stateMachine)
    {
        _speed = _playerMovement.MovementSettings.CrouchData.speed;
        _jumpSpeed = _playerMovement.MovementSettings.CrouchData.jumpSpeed;
        crouchHeight = _playerMovement.MovementSettings.CrouchData.crouchHeight;
    }

    public override void OnEnter()
    {
        _playerMovement.MovementInput.JumpPressed += OnJumpPressed;
        _playerMovement.MovementInput.CrouchReleased += OnCrouchReleased;
        normalHeight = _playerMovement.CharacterController.height;

        SetHeight(true, crouchHeight);
    }


    public override void OnExit()
    {
        _playerMovement.MovementInput.JumpPressed -= OnJumpPressed;
        _playerMovement.MovementInput.CrouchReleased -= OnCrouchReleased;

        SetHeight(false, normalHeight);
    }

    protected void SetHeight(bool isCrouch, float height)
    {
        int sign = isCrouch ? -1 : 1;

        var playerMeshPos = _playerMovement.PlayerMesh.transform.position;
        var cameraPivotPos = _playerMovement.CameraPivot.transform.position;
        var centerPos = _playerMovement.CharacterController.center;

        _playerMovement.PlayerMesh.transform.position = new Vector3(playerMeshPos.x, playerMeshPos.y + sign * crouchHeight, playerMeshPos.z);
        _playerMovement.CameraPivot.transform.position = new Vector3(cameraPivotPos.x, cameraPivotPos.y + sign * crouchHeight, cameraPivotPos.z);
        _playerMovement.CharacterController.center = new Vector3(centerPos.x, centerPos.y + sign * crouchHeight, centerPos.z);

        _playerMovement.CharacterController.height = height;

    }

    protected virtual void OnCrouchReleased()
    {
        
        _playerMovement.ChangeState(_playerMovement.States[MovementStates.Walk]);
    }

    public override void CheckChangeState()
    {
        if (IsGrounded() == false)
        {
            _playerMovement.ChangeState(_playerMovement.States[MovementStates.Fall]);
        }
    }
}
