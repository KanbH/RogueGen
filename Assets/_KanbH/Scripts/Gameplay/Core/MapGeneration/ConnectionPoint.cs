using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionPoint : MonoBehaviour
{
    public enum Direction { North, South, East, West }

    [SerializeField] private Direction connectionDirection;
    
    private bool _connectionUsed = false;

    public GameObject RoomObject { get; set; }

    public void SetConnectionUsed(bool x)
    {
        _connectionUsed = x;
    }

    public bool GetConnectionUsed()
    {
        return _connectionUsed;
    }

    public Direction ConnectionDirection
    {
        get { return connectionDirection; }
        set { connectionDirection = value; }
    }

    public Direction GetConnectionDirection()
    {
        return connectionDirection;
    }

    public Direction GetReverseDirection()
    {
        switch (this.connectionDirection)
        {
            case Direction.North:
                return Direction.South;
            case Direction.South:
                return Direction.North;
            case Direction.East:
                return Direction.West;
            case Direction.West:
                return Direction.East;
            default:
                return Direction.North;
        }
    }

}
