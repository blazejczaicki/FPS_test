using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    [SerializeField] private ParticleSystem _muzzleEffect;

    private void Awake()
    {
        _muzzleEffect.gameObject.SetActive(true);
    }

    public void Activate()
    {
        if (_muzzleEffect.isPlaying ==false)
        {
            _muzzleEffect.Play();
        }        
    }

}
