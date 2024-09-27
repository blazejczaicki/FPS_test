using System;
using System.Collections;
using UnityEngine;



public class HitEffect : PoolObject
{
    public event Action<HitEffect> OnRelease;

    [SerializeField] private ParticleSystem _pfx;
    [SerializeField] private float _effectTime;

    private WaitForSeconds _delay;

    private void Awake()
    {
        _delay = new WaitForSeconds(_effectTime);
    }

    public void PlayEffect()
    {
        _pfx.Emit(1);

        StartCoroutine(TimeToRelease());
    }

    private IEnumerator TimeToRelease()
    {
        yield return _delay;
        OnRelease?.Invoke(this);
    }
}
