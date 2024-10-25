using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateMapBase : MonoBehaviour
{
    [SerializeField] private Tile _baseFloorTile;
    [SerializeField] private Tile _baseWallTile;

    [SerializeField] private GameObject _nodeObject;
    [SerializeField] private NodeManager _nodeManager;
    [SerializeField] private Tilemap _baseFloorTilemap;
    [SerializeField] private Tilemap _baseWallTilemap;

    [SerializeField] private MapSizeSO _mapSizeSO;

    private GameObject[,] _nodeObjects;

    public void InitMapBase()
    {
        _nodeObjects = new GameObject[_mapSizeSO.RightWorldBound - _mapSizeSO.LeftWorldBound + 1, _mapSizeSO.TopWorldBound - _mapSizeSO.BottomWorldBound + 1];
        GenerateMapBase();
        GenerateNodeObjects();
        _nodeManager.SetMapSizeSO(_mapSizeSO);
        _nodeManager.RetrieveNodeObjects(_nodeObjects);
        _nodeManager.UpdateNodesfromTilemap(_baseWallTilemap);
    }

    private void GenerateMapBase()
    {
        for (int i = _mapSizeSO.LeftWorldBound;  i <= _mapSizeSO.RightWorldBound; i++)
        {
            for(int j = _mapSizeSO.BottomWorldBound; j <= _mapSizeSO.TopWorldBound; j++)
            {
                if (i == _mapSizeSO.LeftWorldBound || i  == _mapSizeSO.RightWorldBound || j == _mapSizeSO.BottomWorldBound || j == _mapSizeSO.TopWorldBound)
                {
                    _baseWallTilemap.SetTile(new Vector3Int(i, j, 0), _baseWallTile);
                }
                _baseFloorTilemap.SetTile(new Vector3Int(i, j, 0), _baseFloorTile);
            }
        }
    }

    private void GenerateNodeObjects()
    {
        for (int i = _mapSizeSO.LeftWorldBound; i <= _mapSizeSO.RightWorldBound; i++)
        {
            for (int j = _mapSizeSO.BottomWorldBound; j <= _mapSizeSO.TopWorldBound; j++)
            {
                GameObject newNode = Instantiate(_nodeObject, new Vector3(i+0.5f, j+0.5f, 0), Quaternion.identity, _nodeManager.gameObject.transform);

                _nodeObjects[i,j] = newNode;
            }
        }
    }
}
