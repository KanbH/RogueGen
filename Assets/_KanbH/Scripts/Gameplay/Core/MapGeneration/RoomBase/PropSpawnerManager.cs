using System.Collections.Generic;
using UnityEngine;

public class PropSpawnerManager : MonoBehaviour
{
    [SerializeField] private List<PropSpawner> PropSpawner;

    public void SpawnAllProps()
    {
        if (PropSpawner.Count == 0)
        {
            Debug.LogWarning("No PropSpawners found for this PropSpawnerManager");
            return;
        }

        foreach (var prop in PropSpawner)
        {
            prop.SpawnProp();
        }
    }
}
