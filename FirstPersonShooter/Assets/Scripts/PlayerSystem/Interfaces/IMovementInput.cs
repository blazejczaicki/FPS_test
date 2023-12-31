using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementInput
{
    public Vector2 Move { get; }
    public Vector2 Look { get; }

    public Action JumpPressed { get; set; }
    public Action JumpReleased { get; set; }

    public Action RunPressed { get; set; }
    public Action RunReleased { get; set; }
    public Action CrouchPressed { get; set; }
    public Action CrouchReleased { get; set; }
}
