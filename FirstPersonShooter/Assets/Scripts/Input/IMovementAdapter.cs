using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementAdapter
{
    public Vector2 Move { get; }
    public Vector2 Look { get; }
}
