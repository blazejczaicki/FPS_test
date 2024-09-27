using UnityEngine;
using UnityEngine.Pool;

public abstract class PoolingBase<T> : MonoBehaviour where T : PoolObject
{
    [SerializeField] protected int defaultCapacity;
    [SerializeField] protected int maxSize;

    protected ObjectPool<T> pool;

    protected virtual void Awake()
    {
        pool = new ObjectPool<T>(CreateNewPoolObject, OnGetPoolObject, OnReleasePoolObject, OnDestroyPoolObject, true, defaultCapacity, maxSize);
    }

    protected abstract T CreateNewPoolObject();
    protected abstract void OnGetPoolObject(T poolObject);
    protected abstract void OnReleasePoolObject(T poolObject);
    protected virtual void OnDestroyPoolObject(T poolObject)
    {
        Destroy(poolObject.gameObject);
    }

}
