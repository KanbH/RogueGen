using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    [SerializeField] private List<ConnectionPoint> _connectionPoints = new List<ConnectionPoint>();
    
    [SerializeField] private Tilemap _wallTilemap;

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
    }

    public ConnectionPoint GetUnusedConnection()
    {
        ShufflePointList();
        foreach(ConnectionPoint point in _connectionPoints) 
        {
            if (point.GetConnectionUsed() == false)
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
            if (point.GetConnectionUsed() == false && point.GetReverseDirection() == direction)
            {
                return point;
            }
        }
        return null;
    }

    public List<ConnectionPoint> GetConnectionPoints()
    {
        return _connectionPoints;
    }
}
