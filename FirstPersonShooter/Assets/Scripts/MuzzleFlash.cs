using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    [SerializeField] private ParticleSystem _muzzleEffect;

    private bool _isActive;

    private void Awake()
    {
        Deactive();
    }

    public void Activate()
    {
        if (_isActive == false)
        {
            _muzzleEffect.gameObject.SetActive(true);
            _muzzleEffect.Play();
            _isActive = true;
        }        
    }

    public void Deactive()
    {
        _muzzleEffect.gameObject?.SetActive(false);
        _isActive = false;
    }
}
