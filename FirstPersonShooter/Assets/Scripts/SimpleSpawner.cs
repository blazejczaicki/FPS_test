using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleSpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _enemyCount = 20;
    [SerializeField] private float spawnRadrius = 16;

    public List<Enemy> Enemies { get; set; } = new List<Enemy>();
    private int _spawnCount = 0;

    public void SpawnEnemies()
    {
        _spawnCount = _enemyCount;
        while (_spawnCount > 0)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-spawnRadrius, spawnRadrius), 100, Random.Range(-spawnRadrius, spawnRadrius));
        randomPosition += transform.position;

        RaycastHit hit;
        if (Physics.Raycast(randomPosition, Vector3.down, out hit, Mathf.Infinity))
        {
            var obstacle = hit.collider.GetComponent<NavMeshObstacle>();

            if (obstacle != null)
                return;

            Enemy en = Instantiate(_enemyPrefab, hit.point, Quaternion.identity);
            Enemies.Add(en);
            _spawnCount--;
        }
    }
}
