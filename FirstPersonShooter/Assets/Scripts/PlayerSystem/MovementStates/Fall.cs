using UnityEngine;

public class Fall : MovementState
{
    protected float gravity;
    protected float drag;

    public Fall(StateMachine stateMachine) : base(stateMachine)
    {
        //StateName = MovementStates.Fall;

        gravity = _playerMovement.MovementSettings.FallData.gravity;
        drag = _playerMovement.MovementSettings.FallData.drag;
    }

    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
        _playerMovement.velocity = Vector3.zero;
    }

    public override void CheckChangeState()
    {
        if (IsGrounded())
        {
            _playerMovement.ChangeState(_playerMovement.States[MovementStates.Walk]);
        }
    }


    public override void OnUpdate()
    {
        base.OnUpdate();
        UpdateFall();
    }

    protected void UpdateFall()
    {
        float x = _playerMovement.velocity.x;
        float z = _playerMovement.velocity.z;
        Vector3 movementVector = new Vector3(x, _playerMovement.velocity.y - gravity * Time.deltaTime, z);

        _playerMovement.velocity = movementVector;
        _playerMovement.CharacterController.Move(movementVector * Time.deltaTime);
    }
}
