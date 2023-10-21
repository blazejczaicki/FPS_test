using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneContext : SceneContext
{
    [SerializeField] private SimpleWeaponInventory _weaponInventory;
    [SerializeField] private AudioManager _audioManager;
    private PlayerInputAdapter _playerInputAdapter;

    public static IWeaponInventory WeaponInventory;
    public static IMovementInput MovementInput;
    public static IWeaponInput WeaponInput;

    public static AudioManager AudioManager;

    public override void InstallContext()
    {
        ProjectContext.InputManager.ToggleActionMap(ProjectContext.InputManager.Controls.Player);

        _playerInputAdapter = new PlayerInputAdapter(ProjectContext.InputManager.Controls);

        MovementInput = _playerInputAdapter;
        WeaponInput = _playerInputAdapter;

        WeaponInventory= _weaponInventory;

        AudioManager = _audioManager;
    }

    public override void DisposeContext()
    {
        _playerInputAdapter = null;
        MovementInput = null;
        WeaponInput = null;
        WeaponInventory= null;

        AudioManager = null;
    }
}
