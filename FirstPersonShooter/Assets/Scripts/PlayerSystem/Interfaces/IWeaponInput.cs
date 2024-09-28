using System;

public interface IWeaponInput
{
    public Action FireStarted { get; set; }
    public Action FireReleased { get; set; }

    public Action ChangeWeapon { get; set; }
    public Action AimWeapon { get; set; }
}
