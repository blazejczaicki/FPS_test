using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputAdapter : IMovementAdapter
{
    private Controls _controls;

    public Vector2 Move
    {
        get
        {
            return _controls.Player.Move.ReadValue<Vector2>();
        }
    }

    public Vector2 Look
    {
        get
        {
            return _controls.Player.Look.ReadValue<Vector2>();
        }
    }


    public PlayerInputAdapter(Controls controls)
    {
        _controls = controls;

        Init();
    }

    private void Init()
    {
        _controls.Player.Jump.started += OnJump;
        _controls.Player.Jump.canceled += OnJump;
        _controls.Player.Jump.started += OnCrouch;
        _controls.Player.Jump.canceled += OnCrouch;
        _controls.Player.Jump.started += OnRun;
        _controls.Player.Jump.canceled += OnRun;
    }

    private void OnJump(InputAction.CallbackContext context)
    {

    }

    private void OnCrouch(InputAction.CallbackContext context)
    {

    }

    private void OnRun(InputAction.CallbackContext context)
    {

    }
}
