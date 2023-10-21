using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamagableObject : MonoBehaviour
{
    [field: SerializeField] protected DamagableObjectData DamagableObjectData {  get; private set; }

    public event Action<DamagableObjectData> ObjectDestroy;
    public event Action<DamagableObjectData> ObjectHit;

    public int CurrentHealth { get; protected set; }

    private void Awake()
    {
        CurrentHealth = DamagableObjectData.health;
    }

    public virtual void OnObjectHit(WeaponData weaponData)
    {
        ObjectHit?.Invoke(DamagableObjectData);
    }
    protected virtual void OnObjectDestroy()
    {
        ObjectDestroy?.Invoke(DamagableObjectData);
    }
}
