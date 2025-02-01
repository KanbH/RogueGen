using System.Collections.Generic;
using UnityEngine;

public class PropSpawnerStair : PropSpawner
{
    public override void SpawnProp()
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
            Debug.LogWarning("No valid spawn tiles found for stair prop.");
            return;
        }

        Vector2 spawnPosition = validSpawnPositions[Random.Range(0, validSpawnPositions.Count)];
        Instantiate(PropPrefab, spawnPosition, Quaternion.identity);
    }
}
