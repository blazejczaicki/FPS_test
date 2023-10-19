using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform _weaponHolder;

    private IWeaponInventory _equipedWeapons;
    private IWeaponInput _weaponInput;
    private Weapon _currentWeapon;

    private void Awake()
    {
        
    }

    private void OnDestroy()
    {
        
    }

    private void Start()
    {
        
    }

    public void ChangeWeapon()
    {

    }

    public void Shoot()
    {
        
    }
}
