using System.Collections.Generic;
using UnityEngine;

public class GeneralPropSpawner : PropSpawner
{
    [SerializeField] public GameObject ExitPrefab;

    public override void SpawnProp()
    {
        throw new System.NotImplementedException();
    }

    public void SpawnLevelExit()
    {
        List<Vector2> validSpawnPositions = new List<Vector2>();

        BoundsInt bounds = SpawnTilemap.cellBounds;
        foreach (var position in bounds.allPositionsWithin)
        {
            if (SpawnTilemap.GetTile(position) != null)
            {
                validSpawnPositions.Add(SpawnTilemap.GetCellCenterWorld(position));
            }
        }

        if (validSpawnPositions.Count == 0)
        {
            Debug.LogWarning("No valid spawn tiles found for LevelExit.");
            return;
        }

        Vector2 spawnPosition = validSpawnPositions[Random.Range(0, validSpawnPositions.Count)];
        Instantiate(ExitPrefab, spawnPosition, Quaternion.identity);
    }
}
