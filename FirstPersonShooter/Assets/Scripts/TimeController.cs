using UnityEngine;

[ExecuteInEditMode]
public class TimeController : MonoBehaviour
{
    [SerializeField] private Material _skyboxMat;
    [SerializeField] private Transform _skyboxDirection;
    [SerializeField] private Transform _moonDirection;
    //[SerializeField] private Light _dayLight;
    //[SerializeField] private Light _nightLight;
    [SerializeField] private float _angle = 1f;
    [SerializeField] private bool _isMove = false;


    private float _time = 0;
    private float a = 0;

    private void Update()
    {
        if (_isMove)
            SkyboxRotation();
        SetSkyboxMat();
    }

    private void SkyboxRotation()
    {
        _skyboxDirection.transform.rotation = Quaternion.Euler(a, 0, 0);

        a += _angle * Time.deltaTime;
        if (a > 360)
        {
            a = 0;
        }
    }

    private void SetSkyboxMat()
    {
        _skyboxMat.SetVector("_LightDirForward", _skyboxDirection.forward);
        _skyboxMat.SetVector("_MoonDir", _moonDirection.forward);
        _skyboxMat.SetVector("_LightDirUp", _skyboxDirection.up);
        _skyboxMat.SetVector("_LightDirRight", _skyboxDirection.right);
        _skyboxMat.SetFloat("_NormalizedAngle", a / 360f);
    }
}
