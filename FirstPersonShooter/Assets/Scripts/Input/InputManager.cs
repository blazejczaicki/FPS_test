using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    public event Action<InputActionMap> ActionMapChange;

    public readonly Controls Controls;

    public InputManager()
    {
        Controls = new Controls();
    }

    public void ToggleActionMap(InputActionMap actionMap)
    {
        Controls.Disable();
        ActionMapChange?.Invoke(actionMap);
        actionMap.Enable();
    }
}
