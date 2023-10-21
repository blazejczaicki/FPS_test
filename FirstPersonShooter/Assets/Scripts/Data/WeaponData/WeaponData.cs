using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [SerializeField] public string weaponName;
    [SerializeField] public float damage;
    [SerializeField] public float attackSpeed;
    [SerializeField] public List<ObjectPhysicalMaterials> goodAgainst;
}
