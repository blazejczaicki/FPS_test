using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [field: SerializeField] protected Transform Muzzle { get; private set; }

    protected float _lastShotTime;

    protected bool _isTriggered;
    protected bool _isPuttingOn;
    protected bool _isLoaded;

    public override void OnEnter()
    {
        _lastShotTime=0;

        _isPuttingOn = true;
        _isLoaded = true;

        gameObject.SetActive(true);
        WeaponInput.FireStarted += OnStartFire;
        WeaponInput.FireReleased += OReleasedFire;
    }

    public override void OnExit()
    {
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
            _lastShotTime = 0;
            Shoot();
        }
    }

    protected void Shoot()//layery
    {
        RaycastHit hit;
        if (Physics.Raycast(Muzzle.transform.position, Muzzle.transform.forward, out hit, WeaponData.range))
        {
            Debug.DrawRay(Muzzle.transform.position, Muzzle.transform.forward * WeaponData.range, Color.green, 0.1f);
            var damagableObject = hit.transform.GetComponent<DamagableObject>();

            HitData.damage = WeaponData.damage;
            HitData.goodAgainst = WeaponData.goodAgainst;

            damagableObject?.OnObjectHit(HitData);
        }
    }

    protected void OnStartFire()
    {
        _isTriggered=true;
    }

    protected void OReleasedFire()
    {
        _isTriggered=false;
    }
}
