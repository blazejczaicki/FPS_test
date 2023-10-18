using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementSettings", menuName = "ScriptableObjects/MovementSettings")]
public class MovementSettings : ScriptableObject
{
    [field: SerializeField] public CameraMovementData CameraData { get; set; }    
    [field: SerializeField] public MovementData WalkData { get; set; }
    [field: SerializeField] public MovementData RunData { get; set; }
    [field: SerializeField] public MovementData CrouchData { get; set; }
    [field: SerializeField] public FallData FallData { get; set; }
}
