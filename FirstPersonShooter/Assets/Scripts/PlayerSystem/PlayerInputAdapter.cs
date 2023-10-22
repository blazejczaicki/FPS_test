using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputAdapter : IMovementInput, IWeaponInput
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
    public Action FireStarted { get; set; }
    public Action FireReleased { get; set; }
    public Action ChangeWeapon { get; set; }
    public Action RunPressed { get; set; }
    public Action RunReleased { get; set; }
    public Action CrouchPressed { get; set; }
    public Action CrouchReleased { get; set; }

    public PlayerInputAdapter(Controls controls)
    {
        _controls = controls;


        _controls.Player.Jump.started += OnJump;
        _controls.Player.Jump.canceled += OnJump;
        _controls.Player.Crouch.started += OnCrouch;
        _controls.Player.Crouch.canceled += OnCrouch;
        _controls.Player.Run.started += OnRun;
        _controls.Player.Run.canceled += OnRun;

        _controls.Player.Fire.started += OnFire;
        _controls.Player.Fire.canceled += OnFire;
        _controls.Player.ChangeWeapon.performed += OnChangeGun;
    }

    ~PlayerInputAdapter()
    {
        _controls.Player.Jump.started -= OnJump;
        _controls.Player.Jump.canceled -= OnJump;
        _controls.Player.Fire.started -= OnFire;
        _controls.Player.Fire.canceled -= OnFire;
        _controls.Player.Crouch.started -= OnCrouch;
        _controls.Player.Crouch.canceled -= OnCrouch;
        _controls.Player.Run.started -= OnRun;
        _controls.Player.Run.canceled -= OnRun;
        _controls.Player.ChangeWeapon.performed -= OnChangeGun;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        OnInvoke(context, JumpPressed, JumpReleased);
    }

    private void OnCrouch(InputAction.CallbackContext context)
    {
        OnInvoke(context, CrouchPressed, CrouchReleased);
    }

    private void OnRun(InputAction.CallbackContext context)
    {
        OnInvoke(context, RunPressed, RunReleased);
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        OnInvoke(context, FireStarted, FireReleased);
    }

    private void OnChangeGun(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            ChangeWeapon?.Invoke();
        }
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
