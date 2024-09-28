using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [field: SerializeField] protected WeaponData WeaponData { get; private set; }

    public IWeaponInput WeaponInput { get; set; }
    public HitData HitData { get; set; }

    public LayerMask LayerMask { get; set; }

    public event Action<Vector3, ObjectPhysicalMaterials> Hit;

    protected virtual void Awake()
    {
        HitData = new HitData();
    }

    public abstract void SetEffects(bool isOn);

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();

    public virtual void OnHit(Vector3 hitPos, ObjectPhysicalMaterials material)
    {
        Hit?.Invoke(hitPos, material);
    }

}
