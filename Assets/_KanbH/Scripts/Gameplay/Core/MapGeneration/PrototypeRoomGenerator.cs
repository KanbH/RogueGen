using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class PrototypeRoomGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> _roomPrefabList;
    [SerializeField] private int _roomsToGenerate = 20;
    
    [SerializeField] private CreateMapBase _createMapBase;
    [SerializeField] private NodeManager _nodeManager;
    [SerializeField] private GameObject _mapHolderObject;

    [SerializeField] private MapSizeSO _mapSizeSO;

    private List<GameObject> _roomList = new List<GameObject>();
    private List<ConnectionPoint> _unconnectedPoints = new List<ConnectionPoint>();

    public void StartMapGeneration()
    {
        CenterGenerator();
        GenerateDungeon();
    }

    private void CenterGenerator()
    {
        Vector3 pos = this.gameObject.transform.position;
        pos.x = Mathf.Round(((_mapSizeSO.RightWorldBound + 1) / 2) + _mapSizeSO.LeftWorldBound);
        pos.y = Mathf.Round(((_mapSizeSO.TopWorldBound + 1) / 2) + _mapSizeSO.BottomWorldBound);
        this.gameObject.transform.position = pos;
    }

    private void GenerateDungeon()
    {
        if (_roomsToGenerate < 1) { Debug.LogError("Can't generate less than 1 room!"); }
        GenerateStartRoom();
        RepeatExtend(_roomsToGenerate);
    }

    private void GenerateStartRoom()
    {
        GameObject startRoom = Instantiate(_roomPrefabList[0], this.transform.position, Quaternion.identity, _mapHolderObject.transform);
        Room room = startRoom.GetComponent<Room>();
        AddConnectionPoints(room.GetConnectionPoints());
        _roomList.Add(startRoom);

        if (_nodeManager == null) { Debug.LogError("nodemanager is null"); }
        if ( room.WallTilemap == null ) { Debug.LogError("rwt is null"); }
        if (startRoom == null) { Debug.LogError("startroom isn't null"); }

        _nodeManager.UpdateNodesfromTilemap(room.WallTilemap, startRoom);
    }

    private void RepeatExtend(int times)
    {
        for (int i = 0; i < _roomsToGenerate; i++)
        {
            ExtendDungeonByConnection();
        }
    }
    /*
    private void ExtendDungeon()
    {
        ConnectionPoint originRoomConnection = null;
        ConnectionPoint newRoomConnection = null;
        GameObject originRoom = null;
        GameObject newRoom = null;

        while (originRoomConnection == null)
        {
            //Select random existing room
            originRoom = GetRandomSpawnedRoom();
            //Check and get that room's unused connection point
            if (originRoom.GetComponent<Room>().GetUnusedConnection() != null)
            {
                originRoomConnection = originRoom.GetComponent<Room>().GetUnusedConnection();
                Debug.Log("Room with unused connection chosen!");
            }
        }
        
        while (newRoomConnection == null)
        {
            //Randomly select a prefab 
            GameObject roomToSpawn = GetRandomPrefab();
            //Check if the prefab is compatible with connection point
            if (roomToSpawn.GetComponent<Room>().GetPointToConnect(originRoomConnection.GetConnectionDirection()) != null)
            {
                newRoom = Instantiate(roomToSpawn, this.transform.position, Quaternion.identity);
                newRoomConnection = newRoom.GetComponent<Room>().GetPointToConnect(originRoomConnection.GetConnectionDirection());
                Debug.Log("Prefab succesfully spawned!");
            }
        }

        ConnectRooms(originRoomConnection, newRoom, newRoomConnection);

        bool intersecting = CheckOverlappedExistingRooms(newRoom);

        if (intersecting)
        {
            Destroy(newRoom);
        }
        else
        {
            Debug.Log("This room is successfully spawned!");
            _roomList.Add(newRoom);
            originRoomConnection.SetConnectionUsed(true);
            newRoomConnection.SetConnectionUsed(true);
        }
    }
    */
    private void ExtendDungeonByConnection()
    {
        ConnectionPoint originConnection = null;
        ConnectionPoint newConnection = null;
        GameObject newRoom = null;

        originConnection = GetRandomUnconnectedPoint();
        
        while (newConnection == null)
        {
            GameObject roomToSpawn = GetRandomPrefab();

            if (roomToSpawn.GetComponent<Room>().GetPointToConnect(originConnection.GetConnectionDirection()) != null)
            {
                newRoom = Instantiate(roomToSpawn, this.transform.position, Quaternion.identity, _mapHolderObject.transform);
                newConnection = newRoom.GetComponent<Room>().GetPointToConnect(originConnection.GetConnectionDirection());
            }
        }

        RepositionNewRoom(originConnection, newRoom, newConnection);

        if (CheckOverlappedExistingRooms(newRoom))
        {
            Destroy(newRoom);
        }
        else
        {
            _roomList.Add(newRoom);
            originConnection.SetConnectionUsed(true);
            newConnection.SetConnectionUsed(true);
            AddConnectionPoints(newRoom.GetComponent<Room>().GetConnectionPoints());
            _nodeManager.UpdateNodesfromTilemap(newRoom.GetComponent<Room>().WallTilemap, newRoom);
        }
    }

    /*
    private GameObject GetRandomSpawnedRoom()
    {
        GameObject chosenRoom = _roomList[Random.Range(0, _roomList.Count)];
        return chosenRoom;
    }
    */

    private ConnectionPoint GetRandomUnconnectedPoint()
    {
        ConnectionPoint chosenPoint = _unconnectedPoints[Random.Range(0, _unconnectedPoints.Count)];
        return chosenPoint;
    }

    private GameObject GetRandomPrefab()
    {
        GameObject chosenPrefab = _roomPrefabList[Random.Range(0, _roomPrefabList.Count)];
        return chosenPrefab;
    }

    private void RepositionNewRoom(ConnectionPoint room1Port, GameObject room2, ConnectionPoint room2Port)
    {
        Vector3 room1PortLoc = room1Port.gameObject.transform.position;
        Vector3 offset = room2.transform.position - room2Port.gameObject.transform.position;
        room2.transform.position = room1PortLoc + offset;
    }

    private bool CheckOverlappedExistingRooms(GameObject newRoom) 
    {
        bool intersecting = false;

        //Force refresh colliders
        Physics2D.SyncTransforms();

        Collider2D roomCollider = newRoom.GetComponent<BoxCollider2D>();
        //Debug.Log($"New Room Bounds: {roomCollider.bounds}.");

        foreach (GameObject existingRoom in _roomList)
        {
            Collider2D existingCollider = existingRoom.GetComponent<BoxCollider2D>();
            //Debug.Log($"Origin Room Bounds: {existingCollider.bounds}");
            if (existingCollider.bounds.Intersects(roomCollider.bounds))
            {
                intersecting = true;
                //Debug.Log("Intersecting Bounds! Destroying the room.");
                break;
            }
        }
        return intersecting;
    }

    private void AddConnectionPoints(List<ConnectionPoint> connectionPoints)
    {
        foreach (ConnectionPoint connectionPoint in connectionPoints)
        {
            _unconnectedPoints.Add(connectionPoint);
        }
    }

}
