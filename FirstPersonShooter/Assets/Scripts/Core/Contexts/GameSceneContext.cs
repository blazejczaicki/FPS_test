using UnityEngine;

public class GameSceneContext : SceneContext
{
    [SerializeField] private SimpleWeaponInventory _weaponInventory;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private UI_SimpleWeaponInfo _simpleWeaponInfo;
    [SerializeField] private PlayerMovement _playerMovement;
    private PlayerInputAdapter _playerInputAdapter;

    public static IWeaponInventory WeaponInventory;
    public static IMovementInput MovementInput;
    public static IWeaponInput WeaponInput;

    public static AudioManager AudioManager;
    public static Camera PlayerCamera;
    public static UI_SimpleWeaponInfo SimpleWeaponInfo;
    public static PlayerMovement PlayerMovement;

    public override void InstallContext()
    {
        ProjectContext.InputManager.ToggleActionMap(ProjectContext.InputManager.Controls.Player);

        _playerInputAdapter = new PlayerInputAdapter(ProjectContext.InputManager.Controls);

        MovementInput = _playerInputAdapter;
        WeaponInput = _playerInputAdapter;

        WeaponInventory = _weaponInventory;

        AudioManager = _audioManager;
        PlayerCamera = _playerCamera;

        SimpleWeaponInfo = _simpleWeaponInfo;
        PlayerMovement = _playerMovement;
    }

    public override void DisposeContext()
    {
        _playerInputAdapter = null;
        MovementInput = null;
        WeaponInput = null;
        WeaponInventory = null;

        AudioManager = null;
        PlayerCamera = null;
        SimpleWeaponInfo = null;
        PlayerMovement = null;
    }
}
