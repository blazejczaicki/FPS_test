using UnityEngine;

public class WeaponRecoilEffect : MonoBehaviour
{
    [SerializeField] private float _recoilX = -2;
    [SerializeField] private float _recoilY = 2;
    [SerializeField] private float _recoilZ = 0.35f;
    [SerializeField] private float _snappiness = 6;
    [SerializeField] private float _returnSpeed = 2;

    private Vector3 _currentRot;
    private Vector3 _targetRot;

    public void Update()
    {
        _targetRot = Vector3.Lerp(_targetRot, Vector3.zero, _returnSpeed * Time.deltaTime);
        _currentRot = Vector3.Slerp(_currentRot, _targetRot, _snappiness * Time.deltaTime);

        transform.localRotation = Quaternion.Euler(_currentRot);
    }

    public void Recoil()
    {
        _targetRot -= new Vector3(_recoilX, Random.Range(-_recoilY, _recoilY), Random.Range(-_recoilZ, _recoilZ));
    }
}
