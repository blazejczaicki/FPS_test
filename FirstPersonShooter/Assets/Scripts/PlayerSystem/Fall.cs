using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MovementState
{
    protected float gravity;
    protected float drag;

    public Fall(StateMachine stateMachine) : base(stateMachine)
    {
        gravity = _playerMovement.MovementSettings.FallData.gravity;
        drag = _playerMovement.MovementSettings.FallData.drag;
    }

    public override void CheckChangeState()
    {
        if (IsGrounded())
        {
            _playerMovement.ChangeState(_playerMovement.States[MovementStates.Walk]);
        }
    }

    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
        _playerMovement.velocity = Vector3.zero;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        UpdateFall();
    }

    protected void UpdateFall()
    {
        //float x = Mathf.Lerp(_playerMovement.velocity.x, 0, drag);
        //float z = Mathf.Lerp(_playerMovement.velocity.z, 0, drag);
        float x = _playerMovement.velocity.x;
        float z = _playerMovement.velocity.z;
        Vector3 movementVector = new Vector3( x, _playerMovement.velocity.y - gravity, z);

        _playerMovement.velocity=movementVector;
        _playerMovement.CharacterController.Move(movementVector * Time.deltaTime);
    }
}
