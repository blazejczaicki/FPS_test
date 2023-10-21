using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [field: SerializeField] protected WeaponData WeaponData {get; private set;}
    [field: SerializeField] protected Transform Muzzle {get; private set;}

    public IWeaponInput WeaponInput { get; set; }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}
