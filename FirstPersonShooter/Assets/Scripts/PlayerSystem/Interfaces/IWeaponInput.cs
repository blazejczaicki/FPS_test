using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponInput
{
    public Action FirePerformed { get; set; }
    public Action FireReleased { get; set; }
    public Action ChangeWeapon { get; set; }
}
