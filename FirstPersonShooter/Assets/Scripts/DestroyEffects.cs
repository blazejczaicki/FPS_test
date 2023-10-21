using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class DestroyEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem _destroyEffect;
    [SerializeField] private AudioClip _destroySound;
    private DamagableObject _damagableObject;

    private void Awake()
    {
        _damagableObject = GetComponent<DamagableObject>();
    }

    private void OnEnable()
    {
        _damagableObject.ObjectDestroy += MakeDestroyEffect;
    }

    private void OnDisable()
    {
        _damagableObject.ObjectDestroy -= MakeDestroyEffect;
    }

    private void MakeDestroyEffect(DamagableObject damagableObject)
    {
        GameSceneContext.AudioManager.PlaySound(_destroySound, damagableObject.transform.position);
        damagableObject.gameObject.SetActive(false);
        Instantiate(_destroyEffect, damagableObject.transform.position, Quaternion.identity);
    }

}
