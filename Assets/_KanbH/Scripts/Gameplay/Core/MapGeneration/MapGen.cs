using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    [SerializeField] private CreateMapBase _createMapBase;
    [SerializeField] private NodeManager _nodeManager;
    [SerializeField] private DungeonGen _dungeonGen;
    [SerializeField] private GameObject _mapHolderObject;

    [SerializeField] private MapSizeSO _mapSizeSO;

    [SerializeField] private bool _useSetSeed = false;
    public string StringSeed;
    private int _seed;

    [SerializeField] private int _roomsAmount = 20;
    [SerializeField] private bool _generateStartRoomAtCenter = false;
    [SerializeField] private bool _refundFailedRoomGenerationAttemtps = false;

    private List<GameObject> _roomList = new List<GameObject>();
    private List<ConnectionPoint> _allPoints = new List<ConnectionPoint>();
    private List<ConnectionPoint> _unconnectedPoints = new List<ConnectionPoint>();

    public void StartMapGeneration()
    {
        InitSeed();
        CenterGenerator();
        GenerateDungeon();
    }

    private void InitSeed()
    {
        if (_useSetSeed)
        {
            _seed = StringSeed.GetHashCode();
        }
        else
        {
            _seed = Random.Range(0, 99999);
        }
        Random.InitState(_seed);
        _dungeonGen.InitSeed(_seed);
    }

    //Reposition the generator to be at the center of world boundaries
    private void CenterGenerator()
    {
        Vector3 pos = this.gameObject.transform.position;

        if (_mapSizeSO.RightWorldBound % 2 == 1)
        {
            pos.x = Mathf.Round(((_mapSizeSO.RightWorldBound + 1) / 2) + _mapSizeSO.LeftWorldBound);
        }
        else
        {
            pos.x = Mathf.Round(((_mapSizeSO.RightWorldBound) / 2) + _mapSizeSO.LeftWorldBound);
        }

        if (_mapSizeSO.TopWorldBound % 2 == 1)
        {
            pos.y = Mathf.Round(((_mapSizeSO.TopWorldBound + 1) / 2) + _mapSizeSO.BottomWorldBound);
        }
        else
        {
            pos.y = Mathf.Round(((_mapSizeSO.TopWorldBound) / 2) + _mapSizeSO.BottomWorldBound);
        }

        this.gameObject.transform.position = pos;
    }

    private void GenerateDungeon()
    {
        if (_roomsAmount < 1) { Debug.LogError("Can't generate less than 1 room!"); }
        GenerateStartRoom();
        RepeatExtend(_roomsAmount);
        ActivateAllConnectedPointPrefab();
    }

    private void GenerateStartRoom()
    {
        RoomMetadata startPrefab = GetStartRoomPrefab();
        GameObject startRoom = Instantiate(startPrefab.Prefab, this.transform.position, Quaternion.identity, _mapHolderObject.transform);
        _dungeonGen.AddRoomContribution(startPrefab);

        if (!_generateStartRoomAtCenter)
        {
            startRoom.transform.position = GetRandomStartRoomPosition();
        }

        Room room = startRoom.GetComponent<Room>();
        AddConnectionPoints(room.GetConnectionPoints());
        _roomList.Add(startRoom);

        if (_nodeManager == null) { Debug.LogError("nodemanager is null"); }
        if (room.WallTilemap == null) { Debug.LogError("rwt is null"); }
        if (startRoom == null) { Debug.LogError("startroom isn't null"); }

        _nodeManager.UpdateNodesfromTilemap(room.WallTilemap, startRoom);
    }

    private RoomMetadata GetStartRoomPrefab()
    {
        RoomMetadata chosenPrefab = _dungeonGen.GetRoomPrefabOfType(RoomType.Start);
        return chosenPrefab;
    }

    private RoomMetadata GetRandomPrefab()
    {
        RoomMetadata chosenPrefab = _dungeonGen.GetRoomPrefabOfType(_dungeonGen.SelectNextRoomType());
        return chosenPrefab;
    }

    private Vector2 GetRandomStartRoomPosition()
    {
        float xPosition = Random.Range(_mapSizeSO.LeftWorldBound + _mapSizeSO.MapPadding + 10, _mapSizeSO.RightWorldBound - _mapSizeSO.MapPadding - 10);
        float yPosition = Random.Range(_mapSizeSO.BottomWorldBound + _mapSizeSO.MapPadding + 10, _mapSizeSO.TopWorldBound - _mapSizeSO.MapPadding - 10);
        Vector2 vector2 = new Vector2(xPosition, yPosition);
        return vector2;
    }

    private void RepeatExtend(int times)
    {
        bool generateSuccess;
        for (int i = 0; i < _roomsAmount; i++)
        {
            if (_unconnectedPoints.Count == 0)
            {
                Debug.Log("Can't generate any more room");
                return;
            }
            generateSuccess = ExtendDungeonByConnection();
            if (_refundFailedRoomGenerationAttemtps && generateSuccess == false) { _roomsAmount += 1; }
        }
    }

    private bool ExtendDungeonByConnection()
    {
        ConnectionPoint originConnection = null;
        ConnectionPoint newConnection = null;
        GameObject newRoom = null;
        RoomMetadata newRoomMetadata = null;

        originConnection = GetRandomUnconnectedPoint();
        if (originConnection != null)
        {

            while (newConnection == null)
            {

                RoomMetadata RoomToSpawnMetadata = GetRandomPrefab();
                GameObject roomToSpawn = RoomToSpawnMetadata.Prefab;

                if (roomToSpawn.GetComponent<Room>().GetPointToConnect(originConnection.ConnectionDirection) != null)
                {
                    newRoom = Instantiate(roomToSpawn, this.transform.position, Quaternion.identity, _mapHolderObject.transform);
                    newRoomMetadata = RoomToSpawnMetadata;
                    newConnection = newRoom.GetComponent<Room>().GetPointToConnect(originConnection.ConnectionDirection);
                }
            }

            RepositionNewRoom(originConnection, newRoom, newConnection);

            if (CheckOverlappedExistingRooms(newRoom))
            {
                Debug.Log("Rooms Intersecting :" + newRoom.transform.position);
                _unconnectedPoints.Remove(originConnection);
                Destroy(newRoom);
                return false;
            }
            else
            {
                _roomList.Add(newRoom);
                _dungeonGen.AddRoomContribution(newRoomMetadata);
                originConnection.ConnectionUsed = true;
                newConnection.ConnectionUsed = true;
                AddConnectionPoints(newRoom.GetComponent<Room>().GetConnectionPoints());
                _nodeManager.UpdateNodesfromTilemap(newRoom.GetComponent<Room>().WallTilemap, newRoom);
                return true;
            }
        }
        else { return false; }
    }

    private ConnectionPoint GetRandomUnconnectedPoint()
    {
        if (_unconnectedPoints.Count == 0) { return null; }

        ConnectionPoint chosenPoint = _unconnectedPoints[Random.Range(0, _unconnectedPoints.Count)];
        if (CheckPositionInsideInnerBoundary(chosenPoint.gameObject.transform.position)) { return chosenPoint; }
        else
        {
            _unconnectedPoints.Remove(chosenPoint);
            return null;
        }
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
            _allPoints.Add(connectionPoint);
            _unconnectedPoints.Add(connectionPoint);
        }
    }

    private void ActivateAllConnectedPointPrefab()
    {
        foreach (ConnectionPoint point in _allPoints)
        {
            if (point.ConnectionUsed == true)
            {
                point.CloseObject.SetActive(false);
                point.OpenObject.SetActive(true);

                _nodeManager.UpdateNodesfromTilemap(point.OpenWallTilemap, point.RoomObject);
            }
            else { _nodeManager.UpdateNodesfromTilemap(point.CloseWallTilemap, point.RoomObject); }
        }
    }

    private bool CheckPositionInsideInnerBoundary(Vector2 position)
    {
        if ((_mapSizeSO.LeftWorldBound + _mapSizeSO.MapPadding) < position.x && position.x < (_mapSizeSO.RightWorldBound - _mapSizeSO.MapPadding))
        {
            if ((_mapSizeSO.BottomWorldBound + _mapSizeSO.MapPadding) < position.y && position.y < (_mapSizeSO.TopWorldBound - _mapSizeSO.MapPadding))
            {
                Debug.Log("Position inside inner boundary :" + position);
                return true;
            }
            else { Debug.Log("Outside inner boundary y :" + position.y); }
        }
        else { Debug.Log("Outside inner boundary x :" + position.x); }

        return false;
    }

    private bool CheckPositionInsideOuterBoundary(Vector2 position)
    {
        if ((_mapSizeSO.LeftWorldBound) < position.x && position.x < (_mapSizeSO.RightWorldBound))
        {
            if ((_mapSizeSO.BottomWorldBound) < position.y && position.y < (_mapSizeSO.TopWorldBound))
            {
                Debug.Log("Position inside outer boundary :" + position);
                return true;
            }
            else { Debug.Log("Outside outer boundary y :" + position.y); }
        }
        else { Debug.Log("Outside outer boundary x :" + position.x); }

        return false;
    }
}
