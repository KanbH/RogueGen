using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NodeManager : MonoBehaviour
{
    private MapSizeSO _mapSizeSO;

    private GameObject[,] _nodeGameObjects;
    private Node[,] _nodes;

    public void RetrieveNodeObjects(GameObject[,] nodeGameObjects)
    {
        _nodeGameObjects = nodeGameObjects;
        InitNodesArray(_nodeGameObjects);
    }

    private void InitNodesArray(GameObject[,] nodeGameObjects)
    {
        _nodes = new Node[_mapSizeSO.RightWorldBound - _mapSizeSO.LeftWorldBound + 1, _mapSizeSO.TopWorldBound - _mapSizeSO.BottomWorldBound + 1];

        for (int i = _mapSizeSO.LeftWorldBound; i <= _mapSizeSO.RightWorldBound; i++)
        {
            for (int j = _mapSizeSO.BottomWorldBound; j <= _mapSizeSO.TopWorldBound; j++)
            {
                _nodes[i, j] = _nodeGameObjects[i, j].GetComponent<Node>();

            }
        }
    }

    public void UpdateNodesfromTilemap(Tilemap wallTilemap)
    {
        for (int i = _mapSizeSO.LeftWorldBound; i <= _mapSizeSO.RightWorldBound; i++)
        {
            for (int j = _mapSizeSO.BottomWorldBound; j <= _mapSizeSO.TopWorldBound; j++)
            {
                if (wallTilemap.GetTile(new Vector3Int(i, j, 0)) != null)
                {
                    _nodes[i, j].IsWall = true;
                }
            }
        }
    }

    public void UpdateNodesfromTilemap(Tilemap wallTilemap, GameObject room)
    {
        BoundsInt bounds = wallTilemap.cellBounds;
        Debug.Log(bounds);
        for (int i = bounds.x; i < bounds.xMax; i++)
        {
            for (int j = bounds.y; j < bounds.yMax; j++)
            {
                if (wallTilemap.GetTile(new Vector3Int(i, j, 0)) != null)
                {
                    Debug.Log(wallTilemap.GetTile(new Vector3Int(i, j, 0)));
                    _nodes[i + (int)(room.transform.position.x), j + (int)(room.transform.position.y)].IsWall = true;
                }
            }
        }
    }

    public Node[,] GetNodes() { return _nodes; }

    public void SetMapSizeSO(MapSizeSO mapSizeSO)
    {
        _mapSizeSO = mapSizeSO;
    }
}
