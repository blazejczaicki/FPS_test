using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [field: SerializeField] protected Transform Muzzle { get; private set; }
    [field: SerializeField] protected MuzzleFlash MuzzleFlash { get; private set; }


    protected Camera _camera;

    protected float _lastShotTime;

    protected bool _isTriggered;
    protected bool _isPuttingOn;
    protected bool _isLoaded;

    public override void OnEnter()
    {
        GameSceneContext.SimpleWeaponInfo.SetInfo(WeaponData.goodAgainstMaterial);

        _lastShotTime=0;

        _isPuttingOn = true;
        _isLoaded = true;

        _camera = GameSceneContext.PlayerCamera;

        gameObject.SetActive(true);
        WeaponInput.FireStarted += OnStartFire;
        WeaponInput.FireReleased += OReleasedFire;
    }

    public override void OnExit()
    {
        gameObject.SetActive(false);
        WeaponInput.FireStarted -= OnStartFire;
        WeaponInput.FireReleased -= OReleasedFire;
    }

    public override void OnUpdate()
    {
        _lastShotTime += Time.deltaTime;
        TryShoot();
    }

    protected void TryShoot()
    {
        if (_isTriggered && _isPuttingOn && _isLoaded && _lastShotTime>1f/(WeaponData.firePerMinute/60f))
        {
            MuzzleFlash.Activate();
            _lastShotTime = 0;
            Shoot();
        }
    }

    protected void Shoot()
    {
        GameSceneContext.AudioManager.PlaySound(WeaponData.fireClip, Muzzle.transform.position);
        
        RaycastHit hit;
        if (Physics.Raycast(Muzzle.transform.position, GetFireDirection(), out hit, WeaponData.range, LayerMask))
        {
            OnHit(hit.point);
            Debug.DrawRay(Muzzle.transform.position, GetFireDirection() * WeaponData.range, Color.green, 0.1f);
            var damagableObject = hit.transform.GetComponent<DamagableObject>();

            HitData.damage = WeaponData.damage;
            HitData.goodAgainst = WeaponData.goodAgainstMaterial;

            damagableObject?.OnObjectHit(HitData);
        }
    }

    protected virtual Vector3 GetFireDirection()
    {
        RaycastHit hit;
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask))
        {
            var hitPos = hit.point;
            return (hitPos - Muzzle.transform.position ).normalized;
        }
        return Muzzle.transform.forward;
    }

    protected void OnStartFire()
    {
        _isTriggered=true;
    }

    protected void OReleasedFire()
    {
        MuzzleFlash.Deactive();
        _isTriggered=false;
    }
}
