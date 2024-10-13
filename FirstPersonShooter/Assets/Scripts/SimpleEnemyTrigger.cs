using UnityEngine;

public class SimpleEnemyTrigger : MonoBehaviour
{
    private SimpleSpawner _simpleSpawner;
    private Transform _playerT;

    private void Awake()
    {
        _simpleSpawner = GetComponent<SimpleSpawner>();
    }

    private void Update()
    {
        if (_playerT == null)
            return;

        foreach (Enemy e in _simpleSpawner.Enemies)
        {
            if (e.gameObject.activeSelf)
                e.Agent.destination = _playerT.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _playerT = GameSceneContext.PlayerCamera.transform;
    }
}
