using UnityEngine;

public class Gun : Weapon
{
    [field: SerializeField] protected Transform Muzzle { get; private set; }
    [field: SerializeField] protected MuzzleFlash MuzzleFlash { get; private set; }
    [field: SerializeField] protected WeaponRecoilEffect RecoilEffect { get; private set; }


    protected Camera _camera;
    protected float _lastShotTime;
    protected bool _isTriggered;

    public override void OnEnter()
    {
        GameSceneContext.SimpleWeaponInfo.SetInfo(WeaponData.goodAgainstMaterial);
        _camera = GameSceneContext.PlayerCamera;
        _lastShotTime = 0;

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
        if (_isTriggered && _lastShotTime > 1f / (WeaponData.firePerMinute / 60f))
        {
            _lastShotTime = 0;
            Shoot();
        }
    }

    protected void Shoot()
    {
        MuzzleFlash.Activate();
        RecoilEffect.Recoil();
        GameSceneContext.AudioManager.PlaySound(WeaponData.fireClip, Muzzle.transform.position);

        RaycastHit hit;
        if (Physics.Raycast(Muzzle.transform.position, GetFireDirection(), out hit, WeaponData.range, LayerMask))
        {
            //Debug.DrawRay(Muzzle.transform.position, GetFireDirection() * WeaponData.range, Color.green, 0.1f);
            var damagableObject = hit.transform.GetComponent<DamagableObject>();
            if (damagableObject != null)
            {
                HitData.damage = WeaponData.damage;
                HitData.goodAgainstMaterials = WeaponData.goodAgainstMaterial;
                OnHit(hit.point, damagableObject.DamagableObjectData.physicalMaterial);
                damagableObject?.OnObjectHit(HitData);
            }
            else
            {
                OnHit(hit.point, ObjectPhysicalMaterials.None);
            }
        }
    }

    protected virtual Vector3 GetFireDirection()
    {
        RaycastHit hit;
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask))
        {
            var hitPos = hit.point;
            return (hitPos - Muzzle.transform.position).normalized;
        }
        return Muzzle.transform.forward;
    }

    protected void OnStartFire()
    {
        _isTriggered = true;
    }

    protected void OReleasedFire()
    {
        _isTriggered = false;
    }
}
