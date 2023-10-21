using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWeaponInventory : MonoBehaviour, IWeaponInventory
{
    [SerializeField] private List<Weapon> weapons;

    public Weapon GetWeapon(int index)
    {
        if (weapons.Count==0)
        {
            return null;
        }

        return index>=weapons.Count? weapons[0] : weapons[index];
    }

    public int GetWeaponCount()
    {
        return weapons.Count;
    }

    public void ReturnWeapon(Weapon weapon)
    {
        if(weapon!=null) 
        {
            weapon.transform.SetParent(transform, false);
            weapon.transform.position = Vector3.zero;
            weapon.transform.rotation = Quaternion.identity;
        }
    }
}
