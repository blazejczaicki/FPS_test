using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnviroObject : DamagableObject
{
    public override void OnObjectHit(HitData hitData)
    {
        base.OnObjectHit(hitData);

        if (hitData.goodAgainstMaterials.Contains(DamagableObjectData.physicalMaterial))
        {
            CurrentHealth -= hitData.damage;
            if (CurrentHealth <= 0 && IsDestroyed==false)
            {
                OnObjectDestroy();
                IsDestroyed = true;
            }
        }
    }
}
