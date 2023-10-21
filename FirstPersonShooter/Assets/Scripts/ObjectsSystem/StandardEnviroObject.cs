using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnviroObject : DamagableObject
{
    public override void OnObjectHit(WeaponData weaponData)
    {
        base.OnObjectHit(weaponData);

        if (weaponData.goodAgainst.Contains(DamagableObjectData.physicalMaterial))
        {
            CurrentHealth -= weaponData.damage;
            Debug.Log(CurrentHealth);
            if (CurrentHealth <= 0)
            {
                Debug.Log("BOOM!");
                OnObjectDestroy();
            }
        }
    }
}
