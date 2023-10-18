using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : StateMachine
{
    [field: SerializeField] public Transform CameraPivot { get; private set; }
    [field: SerializeField] public MovementSettings MovementSettings { get; private set; }

    public CharacterController CharacterController { get; protected set; }
    public IMovementAdapter  MovementInput { get; protected set; }
    
    public float verticalAngle { get; set; }

    protected Dictionary<MovementStates, MovementState> states;

    private void Awake()
    {
        states = new Dictionary<MovementStates, MovementState>();
        CharacterController = GetComponent<CharacterController>();

        MovementInput = GameSceneContext.MovementAdapter;
        InitStates();
        
    }

    protected void InitStates()
    {
        states.Add(MovementStates.Walk, new Walk(this));
        states.Add(MovementStates.Run, new Run(this));
        states.Add(MovementStates.Crouch, new Crouch(this));
        states.Add(MovementStates.Fall, new Fall(this));

        ChangeState(states[MovementStates.Walk]);
    }
}
