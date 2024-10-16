using System;
using Unity.AI.Navigation;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private int _cellSize = 40;
    [SerializeField] private SimpleSpawner _simpleSpawner;

    [field: SerializeField] public RoomNeighbours[] Entrances { get; set; }
    [field: SerializeField] public bool IsEnd { get; set; }
    [field: SerializeField] public bool IsConnection { get; set; }

    public void SpawnEnemies()
    {
        if (_simpleSpawner == null)
            return;

        _simpleSpawner.SpawnEnemies();
    }

    public Vector3 GetPositionByDirection(Direction dir)
    {
        switch (dir)
        {
            case Direction.Forward:
                return new Vector3(transform.position.x, transform.position.y, transform.position.z + _cellSize);
            case Direction.Back:
                return new Vector3(transform.position.x, transform.position.y, transform.position.z - _cellSize);
            case Direction.Left:
                return new Vector3(transform.position.x - _cellSize, transform.position.y, transform.position.z);
            case Direction.Right:
                return new Vector3(transform.position.x + _cellSize, transform.position.y, transform.position.z);
            default:
                return transform.position;
        }
    }

}

[Serializable]
public class RoomNeighbours
{
    public Direction direction;
    public bool exist;
}