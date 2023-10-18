using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum MovementStates
{
    Walk,
    Run,
    Crouch,
    Fall
}

public class MovementState : State
{
    protected PlayerMovement _playerMovement;
    protected CameraMovementData _cameraData;

    public MovementState(StateMachine stateMachine)
        : base(stateMachine)
    {
        _playerMovement = stateMachine as PlayerMovement;
        _cameraData = _playerMovement.MovementSettings.CameraData;
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
        UpdateView();
    }

    protected virtual void UpdateView()
    {
        float horizontalRotation = _playerMovement.MovementInput.Look.x * _cameraData.MouseSensitivity;
        float verticalRotation = _playerMovement.MovementInput.Look.y * _cameraData.MouseSensitivity;
        _playerMovement.verticalAngle -= verticalRotation;
        _playerMovement.verticalAngle = Mathf.Clamp(_playerMovement.verticalAngle, _cameraData.minAngle, _cameraData.maxAngle);

        _playerMovement.CharacterController.transform.rotation *=  Quaternion.Euler(0f, horizontalRotation, 0f);
        _playerMovement.CameraPivot.transform.localRotation =  Quaternion.Euler(_playerMovement.verticalAngle, 0f, 0f);
    }
}