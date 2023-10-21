using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamagableObject : MonoBehaviour
{
    [field: SerializeField] public DamagableObjectData DamagableObjectData {  get; private set; }

    public event Action<DamagableObject> ObjectDestroy;
    public event Action<DamagableObject> ObjectHit;

    public int CurrentHealth { get; set; }
    public bool IsDestroyed { get; set; }

    private void Awake()
    {
        CurrentHealth = DamagableObjectData.health;
        IsDestroyed = false;
    }

    public virtual void OnObjectHit(HitData hitData)
    {
        ObjectHit?.Invoke(this);
    }
    protected virtual void OnObjectDestroy()
    {
        ObjectDestroy?.Invoke(this);
    }
}
