using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class PropSpawner : MonoBehaviour
{
    [SerializeField] protected GameObject PropPrefab;
    [SerializeField] protected Tilemap SpawnTilemap;

    public abstract void SpawnProp();
}
