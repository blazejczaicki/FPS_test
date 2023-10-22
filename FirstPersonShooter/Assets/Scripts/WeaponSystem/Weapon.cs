using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [field: SerializeField] protected WeaponData WeaponData {get; private set;}

    public IWeaponInput WeaponInput { get; set; }
    public HitData HitData { get; set; }

    public LayerMask LayerMask { get; set; }

    public event Action<Vector3> Hit;

    private void Awake()
    {
        HitData=new HitData();
    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();

    public virtual void OnHit(Vector3 hitPos)
    {
        Hit?.Invoke(hitPos);
    }

}
