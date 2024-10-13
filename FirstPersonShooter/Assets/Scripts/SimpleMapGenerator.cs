using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum Direction
{
    Forward,
    Back,
    Left,
    Right
}

public class SimpleMapGenerator : MonoBehaviour
{
    [SerializeField] private RoomSetup[] _roomsPrefabs;
    [SerializeField] private RoomSetup _roomsConnectionPrefab;


    private Dictionary<Vector2Int, Room> _rooms;

    private void Awake()
    {
        _rooms = new Dictionary<Vector2Int, Room>();
    }

    private void Start()
    {
        StartCoroutine(GenerateMap());
    }

    public Direction GetOppositeDirection(Direction dir)
    {
        switch (dir)
        {
            case Direction.Forward:
                return Direction.Back;
            case Direction.Back:
                return Direction.Forward;
            case Direction.Left:
                return Direction.Right;
            case Direction.Right:
                return Direction.Left;
            default:
                return Direction.Forward;
        }
    }

    public Quaternion GetRotationByDirection(Direction dir)
    {
        switch (dir)
        {
            case Direction.Forward:
                return Quaternion.Euler(0, 0, 0);
            case Direction.Back:
                return Quaternion.Euler(0, 180, 0);
            case Direction.Left:
                return Quaternion.Euler(0, 270, 0);
            case Direction.Right:
                return Quaternion.Euler(0, 90, 0);
            default:
                return Quaternion.Euler(0, 0, 0);
        }
    }

    public IEnumerator GenerateMap()
    {
        int ind = UnityEngine.Random.Range(0, _roomsPrefabs.Length);
        Room newRoom = Instantiate(_roomsPrefabs[ind].roomPrefab, Vector3.zero, Quaternion.identity, transform);
        _rooms.Add(Vector2Int.zero, newRoom);
        Vector2Int pos = Vector2Int.zero;

        Queue<Vector2Int> unvisited = new Queue<Vector2Int>();
        unvisited.Enqueue(pos);

        while (unvisited.Count > 0)
        {
            Vector2Int currentRoomID = unvisited.Dequeue();
            Room currentRoom = _rooms[currentRoomID];

            foreach (RoomNeighbours entrance in currentRoom.Entrances)
            {
                Vector3 newPos = currentRoom.GetPositionByDirection(entrance.direction);
                if (!entrance.exist && !_rooms.ContainsKey(new Vector2Int((int)newPos.x, (int)newPos.z)))
                {
                    //Room neighbourRoomPrefab = currentRoom.IsConnection ? GetRandomRoom(entrance.direction) : _roomsConnectionPrefab.roomPrefab; //prawidlowe
                    Room neighbourRoomPrefab = newRoom.IsConnection ? GetRandomRoom(entrance.direction) : _roomsConnectionPrefab.roomPrefab;// nie ale fajnie dziala

                    if (neighbourRoomPrefab != null)
                    {
                        Quaternion rot = Quaternion.identity;
                        if (neighbourRoomPrefab.IsEnd)
                            rot = GetRotationByDirection(GetOppositeDirection(entrance.direction));
                        if (neighbourRoomPrefab.IsConnection && (entrance.direction == Direction.Forward || entrance.direction == Direction.Back))
                            rot = Quaternion.Euler(0, 90, 0);


                        newRoom = Instantiate(neighbourRoomPrefab, newPos, rot, transform);
                        pos = new Vector2Int((int)newPos.x, (int)newPos.z);
                        unvisited.Enqueue(pos);
                        _rooms.Add(pos, newRoom);

                        if (neighbourRoomPrefab.IsEnd)
                            newRoom.Entrances[0].direction = GetOppositeDirection(entrance.direction);
                        if (neighbourRoomPrefab.IsConnection && (entrance.direction == Direction.Forward || entrance.direction == Direction.Back))
                        {
                            newRoom.Entrances[0].direction = Direction.Forward;
                            newRoom.Entrances[1].direction = Direction.Back;
                        }

                        RoomNeighbours neiEntr = Array.Find(newRoom.Entrances, e => e.direction == GetOppositeDirection(entrance.direction));
                        if (neiEntr != null)
                            neiEntr.exist = true;

                        entrance.exist = true;
                    }
                }
            }

            yield return new WaitForSeconds(0.1f);
        }

        GenerateNavMesh();
        SpawnEnemies();

    }

    private void GenerateNavMesh()
    {
        _rooms.First().Value.GenerateNavmesh();
    }

    private void SpawnEnemies()
    {
        foreach (var room in _rooms)
        {
            room.Value.SpawnEnemies();
        }
    }

    private Room GetRandomRoom(Direction dir)
    {
        RoomSetup[] rooms = Array.FindAll(_roomsPrefabs, x => x.count > 0 && (x.roomPrefab.IsEnd || x.roomPrefab.IsConnection ||
        x.roomPrefab.Entrances.Any(e => GetOppositeDirection(e.direction) == dir)));

        if (rooms.Length == 0)
            return null;

        int ind = UnityEngine.Random.Range(0, rooms.Length);

        rooms[ind].count--;
        return rooms[ind].roomPrefab;
    }
}


[Serializable]
public class RoomSetup
{
    public int count = 1;
    public Room roomPrefab;
}


//public class SearchGraphDFS : MonoBehaviour
//{
//    [SerializeField] private List<NodeDFS> nodes;
//    [SerializeField] private List<NodeDFS> sources;// which from start

//    public void CheckPath()
//    {
//        List<NodeDFS> sourcesInPath = new List<NodeDFS>(sources.Count);
//        foreach (var source in sources)
//        {
//            if (sourcesInPath.Contains(source) == false)
//            {
//                SearchPaths(sourcesInPath, source);
//            }
//        }
//    }

//    main DFS algo
//    public void SearchPaths(List<NodeDFS> sourcesInPath, NodeDFS currentNode)
//    {
//        var visited = new List<NodeDFS>(nodes.Count);
//        Queue<NodeDFS> unvisited = new Queue<NodeDFS>(nodes.Count);
//        unvisited.Enqueue(currentNode);

//        while (unvisited.Count > 0)
//        {
//            currentNode = unvisited.Dequeue();
//            visited.Add(currentNode);

//            var neighbours = currentNode.GetNeigbhoursWithTile();
//            foreach (var neighbour in neighbours)
//            {
//                if (visited.Contains(neighbour) == false && unvisited.Contains(neighbour) == false)
//                {
//                    unvisited.Enqueue(neighbour);
//                }
//            }
//        }

//        foreach (var slot in visited)
//        {
//            if (slot.IsSource)
//            {
//                sourcesInPath.Add(slot);
//            }
//            _poweredSlots.Add(slot);
//        }
//    }
//}

//public class NodeDFS
//{
//    public List<Direction> AvailableMoveDirections;
//    public List<NodeDFS> GetNeigbhoursWithTile()
//    {
//        List<NodeDFS> neighbours = new List<NodeDFS>();
//        foreach (var direction in AvailableMoveDirections)
//        {
//            var tile = OwnerGrid.GetTileInDirection(this, direction) as NodeDFS; // lub inny getNode

//            if (true)//IsConnection(tile, direction) warunek po³¹czeñ
//            {
//                neighbours.Add(tile);
//            }
//        }
//        return neighbours;
//    }
//}
