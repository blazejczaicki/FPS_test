using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [SerializeField] public string weaponName;
    [SerializeField] public AudioClip fireClip;
    [SerializeField] public int ammo;
    [SerializeField] public int damage;
    [SerializeField] public float firePerMinute;
    [SerializeField] public float range;
    

    [SerializeField] public List<ObjectPhysicalMaterials> goodAgainstMaterial;
}
