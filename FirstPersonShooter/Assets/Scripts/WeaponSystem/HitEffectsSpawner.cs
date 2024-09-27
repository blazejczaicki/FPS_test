using UnityEngine;

public class HitEffectsSpawner : PoolingBase<HitEffect>
{
    [SerializeField] private HitEffect _effectPrefab;

    private Vector3 _positionEffect = Vector3.zero;
    private Vector3 _normalEffect = Vector3.zero;

    public void ReleaseEffect(HitEffect objEffect)
    {
        pool.Release(objEffect);
    }

    public void SpawnEffect(Vector3 position, Vector3 normal)
    {
        _positionEffect = position;
        _normalEffect = normal;
        pool.Get();
    }

    protected override HitEffect CreateNewPoolObject()
    {
        Debug.Log("Create");
        HitEffect hitEffect = Instantiate(_effectPrefab, _positionEffect, Quaternion.identity, transform);
        hitEffect.OnRelease += ReleaseEffect;
        hitEffect.transform.forward = _normalEffect;
        hitEffect.PlayEffect();
        return hitEffect;
    }

    protected override void OnGetPoolObject(HitEffect poolObject)
    {
        Debug.Log("Get");
        poolObject.gameObject.SetActive(true);
        poolObject.transform.position = _positionEffect;
        poolObject.transform.forward = _normalEffect;
        poolObject.PlayEffect();
    }

    protected override void OnReleasePoolObject(HitEffect poolObject)
    {
        Debug.Log("Release");
        poolObject.gameObject.SetActive(false);
    }

    protected override void OnDestroyPoolObject(HitEffect poolObject)
    {
        poolObject.OnRelease -= ReleaseEffect;
        base.OnDestroyPoolObject(poolObject);
    }
}
