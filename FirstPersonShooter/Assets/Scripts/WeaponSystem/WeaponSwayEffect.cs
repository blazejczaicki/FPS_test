using UnityEngine;

public class WeaponSwayEffect : MonoBehaviour
{
    [SerializeField] private float _smooth = 10f;
    [SerializeField] private float _smoothRot = 10f;

    [SerializeField] private float step = 0.01f;
    [SerializeField] private float maxStepDistance = 0.06f;
    [SerializeField] private float rotationStep = 4f;
    [SerializeField] private float maxRotationStep = 5f;

    private Vector3 _swayPos;
    private Vector3 _swayRot;

    private Vector3 _defaultPos;
    private Quaternion _defaultRot;

    private void Awake()
    {
        _defaultRot = transform.localRotation;
        _defaultPos = transform.localPosition;
    }

    public void OnUpdate(Vector2 lookInput)
    {
        Sway(lookInput);
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

        transform.localPosition = Vector3.Lerp(transform.localPosition, _defaultPos + _swayPos, Time.deltaTime * _smoothRot);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, _defaultRot * Quaternion.Euler(_swayRot), Time.deltaTime * _smoothRot);
    }
}
