using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : StateMachine
{
    [field: SerializeField] public Transform CameraPivot { get; private set; }
    [field: SerializeField] public Transform PlayerMesh { get; private set; }
    [field: SerializeField] public MovementSettings MovementSettings { get; private set; }

    [field: SerializeField] public HeadbobEffect HeadbobEffect { get; private set; }
    [field: SerializeField] public WeaponSwayEffect WeaponSwayEffect { get; private set; }

    public CharacterController CharacterController { get; protected set; }
    public Dictionary<MovementStates, MovementState> States { get; protected set; }
    public IMovementInput MovementInput { get; protected set; }
    public float verticalAngle { get; set; }
    public Vector3 velocity { get; set; }


    private void Awake()
    {
        States = new Dictionary<MovementStates, MovementState>();
        CharacterController = GetComponent<CharacterController>();
        velocity = Vector3.zero;

        MovementInput = GameSceneContext.MovementInput;
        InitStates();

    }


    protected void InitStates()
    {
        States.Add(MovementStates.Walk, new Walk(this));
        States.Add(MovementStates.Run, new Run(this));
        States.Add(MovementStates.Crouch, new Crouch(this));
        States.Add(MovementStates.Fall, new Fall(this));

        ChangeState(States[MovementStates.Walk]);
    }

}
