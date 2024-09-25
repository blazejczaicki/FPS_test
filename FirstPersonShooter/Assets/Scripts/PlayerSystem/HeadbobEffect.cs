using UnityEngine;

public class HeadbobEffect : MonoBehaviour
{
    [Range(0.001f, 0.01f)][SerializeField] private float _amount = 0.002f;
    [Range(1, 30)][SerializeField] private float _frequency = 10f;
    [Range(10, 100)][SerializeField] private float _smooth = 10f;

    private Vector3 _startPos;
    private void Start()
    {
        _startPos = transform.localPosition;
    }

    public void OnUpdate(bool IsMove)
    {
        if (IsMove)
            StartEffect();
        StopEffect();
    }

    private void StartEffect()
    {
        Vector3 pos = Vector3.zero;
        pos.y = Mathf.Lerp(pos.y, Mathf.Sin(Time.time * _frequency) * _amount * 1.4f, _smooth * Time.deltaTime);
        pos.x = Mathf.Lerp(pos.x, Mathf.Cos(Time.time * _frequency * 0.5f) * _amount * 1.6f, _smooth * Time.deltaTime);
        transform.localPosition += pos;
    }

    private void StopEffect()
    {
        if (transform.localPosition == _startPos)
            return;
        transform.localPosition = Vector3.Lerp(transform.localPosition, _startPos, Time.deltaTime);
    }
}
