using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> GenericEnemyPrefabs; //Assigned by DungeonGen
    [SerializeField] public List<GameObject> SpecialEnemyPrefabs; //Assigned in inspector based on each room's characteristic

    public int RoomMaxThreatLevel { private get; set; }



}
