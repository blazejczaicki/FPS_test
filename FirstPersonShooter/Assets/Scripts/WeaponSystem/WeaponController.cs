using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform _weaponHolder;
    [SerializeField] private LayerMask _layerMask;

    public Weapon CurrentWeapon { get; private set; }

    private IWeaponInventory _weaponsInventory;
    private IWeaponInput _weaponInput;
    
    private int _maxWeaponIndex;
    private int _weaponIndex;

    private void Awake()
    {
        _weaponInput = GameSceneContext.WeaponInput;
        _weaponsInventory = GameSceneContext.WeaponInventory;

        _weaponInput.ChangeWeapon += ChangeWeapon;
    }

    private void OnDestroy()
    {
        _weaponInput.ChangeWeapon -= ChangeWeapon;
    }

    private void Start()
    {
        _maxWeaponIndex = _weaponsInventory.GetWeaponCount();
        _weaponIndex = 0;
    }

    private void Update()
    {
        CurrentWeapon?.OnUpdate();
    }

    public void ChangeWeapon()
    {
        CurrentWeapon?.OnExit();

        _weaponIndex = _weaponIndex + 1>=_maxWeaponIndex ? 0 : _weaponIndex + 1;

        _weaponsInventory.ReturnWeapon(CurrentWeapon);
        CurrentWeapon = _weaponsInventory.GetWeapon(_weaponIndex);
        SetWeapon();
    }

    public void SetWeapon()
    {
        if (CurrentWeapon != null)
        {
            CurrentWeapon.transform.SetParent(_weaponHolder, false);
            CurrentWeapon.WeaponInput = _weaponInput;
            CurrentWeapon.LayerMask = _layerMask;
            CurrentWeapon.OnEnter();
        }
    }
}
