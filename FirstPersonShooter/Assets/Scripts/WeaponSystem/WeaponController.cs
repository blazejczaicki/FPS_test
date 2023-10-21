using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform _weaponHolder;

    private IWeaponInventory _weaponsInventory;
    private IWeaponInput _weaponInput;
    private Weapon _currentWeapon;
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
        _currentWeapon?.OnUpdate();
    }

    public void ChangeWeapon()
    {
        _currentWeapon?.OnExit();

        _weaponIndex = _weaponIndex + 1>=_maxWeaponIndex ? 0 : _weaponIndex + 1;

        _weaponsInventory.ReturnWeapon(_currentWeapon);
        _currentWeapon = _weaponsInventory.GetWeapon(_weaponIndex);
        SetWeapon();
    }

    public void SetWeapon()
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.transform.SetParent(_weaponHolder, false);
            //_currentWeapon.transform.position = Vector3.zero;
            _currentWeapon.WeaponInput = _weaponInput;
            _currentWeapon.OnEnter();
        }
    }
}
