using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ConnectionPoint : MonoBehaviour
{
    public enum Direction { North, South, East, West }

    [SerializeField] private Direction connectionDirection;
    
    public bool ConnectionUsed { get; set; } = false;

    public GameObject RoomObject { get; set; }

    //by Default the Close Prefab is activated but both variant's tilemap is only updated after Dungeon finished the generation
    public GameObject OpenObject;
    public Tilemap OpenWallTilemap;
    public GameObject CloseObject;
    public Tilemap CloseWallTilemap;

    public Direction ConnectionDirection
    {
        get { return connectionDirection; }
        set { connectionDirection = value; }
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
