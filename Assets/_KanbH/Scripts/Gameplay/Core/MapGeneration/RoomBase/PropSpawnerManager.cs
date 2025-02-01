using System.Collections.Generic;
using UnityEngine;

public class PropSpawnerManager : MonoBehaviour
{
    [SerializeField] private List<PropSpawner> PropSpawner;
    [SerializeField] private GeneralPropSpawner GeneralPropSpawner;

    public void SpawnAllProps()
    {
        foreach (var prop in PropSpawner)
        {
            prop.SpawnProp();
        }
    }

    public void SpawnLevelExit()
    {
        GeneralPropSpawner.SpawnLevelExit();
    }
}
