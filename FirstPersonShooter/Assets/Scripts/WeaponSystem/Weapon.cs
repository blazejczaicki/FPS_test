using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [field: SerializeField] protected WeaponData WeaponData {get; private set;}
    [field: SerializeField] protected Transform Muzzle {get; private set;}

    public abstract void OnEquipWeapon();
    public abstract void OnShoot();
    public abstract void OnReload();
}
