using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputAdapter : IMovementInput
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

    public Action JumpPressed { get; set; }
    public Action JumpReleased { get; set; }

    public PlayerInputAdapter(Controls controls)
    {
        _controls = controls;

        _controls.Player.Jump.started += OnJump;
        _controls.Player.Jump.canceled += OnJump;
        //_controls.Player.Crouch.started += OnCrouch;
        //_controls.Player.Crouch.canceled += OnCrouch;
        //_controls.Player.Run.started += OnRun;
        //_controls.Player.Run.canceled += OnRun;
    }

    ~PlayerInputAdapter()
    {
        _controls.Player.Jump.started -= OnJump;
        _controls.Player.Jump.canceled -= OnJump;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        OnInvoke(context, JumpPressed, JumpReleased);
    }

    private void OnCrouch(InputAction.CallbackContext context)
    {

    }

    private void OnRun(InputAction.CallbackContext context)
    {
        //OnInvoke( context, inputEvent);
    }

    private void OnInvoke(InputAction.CallbackContext context, Action pressedEvent, Action releasedEvent)
    {
        if (context.started)
        {
            pressedEvent?.Invoke();
        }
        else if (context.canceled)
        {
            releasedEvent?.Invoke();
        }
    }
}
