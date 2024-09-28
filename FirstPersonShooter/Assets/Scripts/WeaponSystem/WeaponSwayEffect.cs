using UnityEngine;

public class WeaponSwayEffect : MonoBehaviour
{
    [SerializeField] private float _smooth = 6f;
    [SerializeField] private float _smoothRot = 6f;

    [SerializeField] private float step = 0.005f;
    [SerializeField] private float maxStepDistance = 0.03f;
    [SerializeField] private float rotationStep = 1f;
    [SerializeField] private float maxRotationStep = 3f;

    [SerializeField] private float _staticFrequency = 1f;
    [SerializeField] private float _amplitude = 0.005f;

    private Vector3 _swayPos;
    private Vector3 _swayRot;

    private Vector3 _defaultPos;
    private Quaternion _defaultRot;

    private bool _isStart = true;

    private void Awake()
    {
        _defaultRot = transform.localRotation;
        _defaultPos = transform.localPosition;
    }

    public void OnUpdate(Vector2 lookInput)
    {
        if (_isStart)
            Sway(lookInput);
    }

    public void StopEffect()
    {
        _isStart = false;
        transform.localPosition = transform.localPosition;
        transform.localRotation = transform.localRotation;
    }

    public void RestartEffect()// ulepszyc te defaulty bo zostaje jak siê z odchylenia robi
    {
        _isStart = true;
        _defaultRot = transform.localRotation;
        _defaultPos = transform.localPosition;
    }

    private void Sway(Vector2 lookInput)
    {
        _swayPos = new Vector3(lookInput.x, lookInput.y, 0) * -step;
        _swayPos.x = Mathf.Clamp(_swayPos.x, -maxStepDistance, maxStepDistance);
        _swayPos.y = Mathf.Clamp(_swayPos.y, -maxStepDistance, maxStepDistance);

        _swayRot = lookInput * -rotationStep;
        _swayRot.x = Mathf.Clamp(_swayRot.x, -maxRotationStep, maxRotationStep);
        _swayRot.y = Mathf.Clamp(_swayRot.y, -maxRotationStep, maxRotationStep);
        _swayRot = new Vector3(_swayRot.y, _swayRot.x, _swayRot.x);

        if (lookInput == Vector2.zero)
            _swayPos.y = Mathf.Sin(Time.time * _staticFrequency) * _amplitude;

        transform.localPosition = Vector3.Lerp(transform.localPosition, _defaultPos + _swayPos, Time.deltaTime * _smoothRot);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, _defaultRot * Quaternion.Euler(_swayRot), Time.deltaTime * _smoothRot);
    }
}
