using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponInventory
{
    public int GetWeaponCount();
    public Weapon GetWeapon(int index);    
    public void ReturnWeapon(Weapon weapon);    
}
