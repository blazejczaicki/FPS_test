using UnityEngine;

public class Gun : Weapon
{
    [field: SerializeField] protected Transform Muzzle { get; private set; }
    [field: SerializeField] protected MuzzleFlash MuzzleFlash { get; private set; }
    [field: SerializeField] protected WeaponRecoilEffect RecoilEffect { get; private set; }
    [field: SerializeField] protected ParticleSystem BulletCasingsEffect { get; private set; }
    [field: SerializeField] protected ParticleSystem ShootTrailEffect { get; private set; }
    [field: SerializeField] protected WeaponSwayEffect WeaponSwayEffect { get; private set; }

    [SerializeField] protected float _maxRecoilTime = 1f;
    [SerializeField] protected float _recoilSize = 0.08f;


    protected Camera _camera;
    protected float _lastShotTime;
    protected float _pressedTime;
    protected bool _isTriggered;

    protected override void Awake()
    {
        base.Awake();
        WeaponSwayEffect = transform.parent.GetComponent<WeaponSwayEffect>();
    }

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
            _pressedTime += Time.deltaTime;
            _lastShotTime = 0;
            Shoot();
        }
    }

    protected void Shoot()
    {
        MuzzleFlash.Activate();
        RecoilEffect.Recoil();
        GameSceneContext.AudioManager.PlaySound(WeaponData.fireClip, Muzzle.transform.position);
        BulletCasingsEffect.Emit(1);
        ShootTrailEffect.Emit(1);

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

            GameSceneContext.HitEffectsSpawner.SpawnEffect(hit.point, hit.normal);
        }
    }

    protected virtual Vector3 GetFireDirection()
    {
        RaycastHit hit;
        Ray ray = new Ray(_camera.transform.position, RecoilDirection());
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask))
        {
            var hitPos = hit.point;
            return (hitPos - Muzzle.transform.position).normalized;
        }
        return Muzzle.transform.forward;
    }

    protected virtual Vector3 RecoilDirection()
    {
        Vector3 dir = _camera.transform.forward;
        dir.x += (Random.value - 0.5f) * _recoilSize;
        dir.z += (Random.value - 0.5f) * _recoilSize;
        dir.y += (Random.value - 0.5f) * _recoilSize; // _ is trigger + _pressedTime
        return dir;
    }

    protected void OnStartFire()
    {
        _pressedTime = 0;
        _isTriggered = true;
    }

    protected void OReleasedFire()
    {
        _isTriggered = false;
    }

    public override void SetEffects(bool isOn)
    {
        if (isOn)
        {
            WeaponSwayEffect.RestartEffect();
        }
        else
        {
            WeaponSwayEffect.StopEffect();
        }
    }
}
