using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    [SerializeField] public RoomMetadata RoomMetadata;
    [SerializeField] private List<ConnectionPoint> _connectionPoints = new List<ConnectionPoint>();
    [SerializeField] private Tilemap _wallTilemap;
    [SerializeField] private PropSpawnerManager _propSpawnerManager;
    [SerializeField] private EnemySpawnerManager _enemySpawnerManager;

    public Tilemap WallTilemap
    {
        get { return _wallTilemap; }
    }

    private void Awake()
    {
        foreach (var connectionPoint in _connectionPoints)
        {
            connectionPoint.RoomObject = this.gameObject;
        }
        if (!(_propSpawnerManager != null))
        {
            _propSpawnerManager = this.gameObject.GetComponent<PropSpawnerManager>();
        }
        if (!(_enemySpawnerManager != null))
        {
            _enemySpawnerManager = this.gameObject.GetComponent<EnemySpawnerManager>();
        }

        _enemySpawnerManager.RoomMaxThreatLevel = RoomMetadata.MaxThreatLevel;
    }

    public ConnectionPoint GetUnusedConnection()
    {
        ShufflePointList();
        foreach (ConnectionPoint point in _connectionPoints)
        {
            if (point.ConnectionUsed == false)
            {
                return point;
            }
        }
        return null;
    }

    private void ShufflePointList()
    {
        for (int i = _connectionPoints.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            ConnectionPoint temp = _connectionPoints[i];
            _connectionPoints[i] = _connectionPoints[j];
            _connectionPoints[j] = temp;
        }
    }

    public ConnectionPoint GetPointToConnect(ConnectionPoint.Direction direction)
    {
        foreach (ConnectionPoint point in _connectionPoints)
        {
            if (point.ConnectionUsed == false && point.GetReverseDirection() == direction)
            {
                return point;
            }
        }
        return null;
    }

    public void SpawnAllRoomProps()
    {
        _propSpawnerManager.SpawnAllProps();
    }

    public List<ConnectionPoint> GetConnectionPoints()
    {
        return _connectionPoints;
    }
}
