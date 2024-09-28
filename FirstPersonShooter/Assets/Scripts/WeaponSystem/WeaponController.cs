using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform _weaponHolder;
    [SerializeField] private Transform _weaponPos;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _weaponAimedPos;
    [SerializeField] private Transform _weaponDefaultPos;
    [SerializeField] private float _animTime = 10;

    public Weapon CurrentWeapon { get; private set; }

    private IWeaponInventory _weaponsInventory;
    private IWeaponInput _weaponInput;

    private IEnumerator _currentCorutine;
    private int _maxWeaponIndex;
    private int _weaponIndex;
    private bool _isAimed;

    private void Awake()
    {
        _weaponInput = GameSceneContext.WeaponInput;
        _weaponsInventory = GameSceneContext.WeaponInventory;


    }

    private void OnEnable()
    {
        _weaponInput.ChangeWeapon += ChangeWeapon;
        _weaponInput.AimWeapon += AimWeapon;
    }

    private void OnDisable()
    {
        _weaponInput.ChangeWeapon -= ChangeWeapon;
        _weaponInput.AimWeapon -= AimWeapon;
    }

    private void Start()
    {
        _maxWeaponIndex = _weaponsInventory.GetWeaponCount();
        _weaponIndex = 0;
    }

    private void Update()
    {
        CurrentWeapon?.OnUpdate();
    }

    public void ChangeWeapon()
    {
        CurrentWeapon?.OnExit();

        _weaponIndex = _weaponIndex + 1 >= _maxWeaponIndex ? 0 : _weaponIndex + 1;

        _weaponsInventory.ReturnWeapon(CurrentWeapon);
        CurrentWeapon = _weaponsInventory.GetWeapon(_weaponIndex);
        SetWeapon();
    }

    private void SetWeapon()
    {
        if (CurrentWeapon != null)
        {
            CurrentWeapon.transform.SetParent(_weaponHolder, false);
            CurrentWeapon.WeaponInput = _weaponInput;
            CurrentWeapon.LayerMask = _layerMask;
            CurrentWeapon.OnEnter();
        }
    }

    private void AimWeapon()
    {
        _isAimed = !_isAimed;
        if (_isAimed)
        {
            //StopCoroutine(_currentCorutine);
            //_currentCorutine = AnimAiming(_weaponDefaultPos, _weaponAimedPos);
            //StartCoroutine(_currentCorutine);
            StartCoroutine(AnimAiming(_weaponDefaultPos, _weaponAimedPos, 60, 50));
        }
        else
        {
            //StopCoroutine(_currentCorutine);
            //_currentCorutine = AnimAiming(_weaponAimedPos, _weaponDefaultPos);
            //StartCoroutine(_currentCorutine);
            StartCoroutine(AnimAiming(_weaponAimedPos, _weaponDefaultPos, 50, 60));
        }
    }

    private IEnumerator AnimAiming(Transform start, Transform end, float startFov, float endFov)
    {
        CurrentWeapon.SetEffects(false);
        float t = 0f;
        Vector3 lerp = Vector3.zero;
        while ((_weaponPos.position - end.position).magnitude > 0.0001f)
        {
            t += _animTime * Time.deltaTime;
            t = Mathf.Clamp01(t);
            lerp = Vector3.Lerp(start.position, end.position, t);
            _weaponHolder.position = lerp;
            _weaponPos.position = lerp;
            GameSceneContext.PlayerCamera.fieldOfView = Mathf.Lerp(startFov, endFov, t);
            yield return null;
        }
        CurrentWeapon.SetEffects(true);
        Debug.Log("Done");
    }
}
