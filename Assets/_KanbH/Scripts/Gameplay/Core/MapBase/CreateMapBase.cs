using UnityEngine;
using UnityEngine.Tilemaps;
using NUnit.Framework;

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

    private int _leftWorldBound;
    private int _rightWorldBound;
    private int _bottomWorldBound;
    private int _topWorldBound;

    #region Awake
    private void Awake()
    {
        CheckComponentsNotNull();
        CheckFieldsNotNull();
        CacheSOValues();
    }
    private void CheckComponentsNotNull()
    {
        Assert.IsNotNull(_nodeObject);
        Assert.IsNotNull(_nodeManager);
        Assert.IsNotNull(_baseFloorTilemap);
        Assert.IsNotNull(_baseWallTilemap);
    }

    private void CheckFieldsNotNull()
    {
        Assert.IsNotNull(_baseFloorTile);
        Assert.IsNotNull(_baseWallTile);
        Assert.IsNotNull(_mapSizeSO);
    }

    private void CacheSOValues()
    {
        _leftWorldBound = _mapSizeSO.LeftWorldBound;
        _rightWorldBound = _mapSizeSO.RightWorldBound;
        _bottomWorldBound = _mapSizeSO.BottomWorldBound;
        _topWorldBound = _mapSizeSO.TopWorldBound;
    }
    #endregion

    public void InitMapBase()
    {
        _nodeObjects = new GameObject[_rightWorldBound - _leftWorldBound + 1, _topWorldBound - _bottomWorldBound + 1];
        GenerateMapBase();
        GenerateNodeObjects();
        _nodeManager.SetMapSizeSO(_mapSizeSO);
        _nodeManager.RetrieveNodeObjects(_nodeObjects);
        _nodeManager.UpdateNodesfromTilemap(_baseWallTilemap);
    }

    private void GenerateMapBase()
    {
        for (int i = _leftWorldBound; i <= _rightWorldBound; i++)
        {
            for (int j = _bottomWorldBound; j <= _topWorldBound; j++)
            {
                if (i == _leftWorldBound || i == _rightWorldBound || j == _bottomWorldBound || j == _topWorldBound)
                {
                    _baseWallTilemap.SetTile(new Vector3Int(i, j, 0), _baseWallTile);
                }
                _baseFloorTilemap.SetTile(new Vector3Int(i, j, 0), _baseFloorTile);
            }
        }
    }

    private void GenerateNodeObjects()
    {
        for (int i = _leftWorldBound; i <= _rightWorldBound; i++)
        {
            for (int j = _bottomWorldBound; j <= _topWorldBound; j++)
            {
                GameObject newNode = Instantiate(_nodeObject, new Vector3(i + 0.5f, j + 0.5f, 0), Quaternion.identity, _nodeManager.gameObject.transform);

                _nodeObjects[i, j] = newNode;
            }
        }
    }
}
