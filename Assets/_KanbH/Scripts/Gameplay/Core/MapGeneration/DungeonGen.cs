using System.Collections.Generic;
using UnityEngine;

public class DungeonGen : MonoBehaviour
{
    [Header("Dungeon Settings")]
    [SerializeField] private List<RoomMetadata> StartRooms;
    [SerializeField] private List<RoomMetadata> CombatRooms;
    [SerializeField] private List<RoomMetadata> Corridors;
    [SerializeField] private List<RoomMetadata> TreasureRooms;
    [SerializeField] private List<RoomMetadata> OddityRooms;

    private Dictionary<RoomType, List<RoomMetadata>> _roomPrefabs = new Dictionary<RoomType, List<RoomMetadata>>();

    private bool _initialized = false;

    public void InitSeed(int seed)
    {
        if (_initialized == false)
        {
            Random.InitState(seed);
            SetupDict();
            _initialized = true;
        }
    }

    private void SetupDict()
    {
        _roomPrefabs.Add(RoomType.Start, StartRooms);
        _roomPrefabs.Add(RoomType.Combat, CombatRooms);
        _roomPrefabs.Add(RoomType.Corridor, Corridors);
        _roomPrefabs.Add(RoomType.Treasure, TreasureRooms);
        _roomPrefabs.Add(RoomType.Oddity, OddityRooms);
    }

    public GameObject GetRoomPrefab(RoomType roomType)
    {
        if (_initialized == false) { Debug.LogError("Rooms dictionary not setup yet"); }
        List<RoomMetadata> roomTypePrefabs = _roomPrefabs[roomType];
        if (roomTypePrefabs.Count > 0)
        {
            float totalWeight = 0f;

            // Calculate total weight
            foreach (var prefabWeight in roomTypePrefabs)
            {
                totalWeight += prefabWeight.Weight;
            }

            // Pick a random value within the total weight range
            float randomValue = Random.Range(0f, totalWeight);

            // Iterate and find the corresponding prefab
            float cumulativeWeight = 0f;
            foreach (var prefabWeight in roomTypePrefabs)
            {
                cumulativeWeight += prefabWeight.Weight;
                if (randomValue <= cumulativeWeight)
                {
                    return prefabWeight.Prefab;
                }
            }
        }

        Debug.LogWarning($"No prefabs available for room type: {roomType}");
        return null;
    }
}
