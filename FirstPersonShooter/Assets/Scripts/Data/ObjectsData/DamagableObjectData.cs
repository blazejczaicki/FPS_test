using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamagableObjectData", menuName = "ScriptableObjects/DamagableObjectData")]
public class DamagableObjectData : ScriptableObject
{
    [SerializeField] public string objectName;
    [SerializeField] public int health;

    [SerializeField] public ObjectPhysicalMaterials physicalMaterial;
}
