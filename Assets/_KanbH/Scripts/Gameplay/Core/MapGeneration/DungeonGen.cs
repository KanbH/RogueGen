using System.Collections.Generic;
using UnityEngine;

public class DungeonGen : MonoBehaviour
{
    [Header("Dungeon Settings")]
    [SerializeField] private float CombatRatio;
    [SerializeField] private float TreasureRatio;
    [SerializeField] private float CorridorRatio;
    [SerializeField] private float OddityRatio;

    [Header("Prefab Metadatas")]
    [SerializeField] private List<RoomMetadata> StartRooms;
    [SerializeField] private List<RoomMetadata> CombatRooms;
    [SerializeField] private List<RoomMetadata> TreasureRooms;
    [SerializeField] private List<RoomMetadata> Corridors;
    [SerializeField] private List<RoomMetadata> OddityRooms;

    private Dictionary<RoomType, List<RoomMetadata>> _roomPrefabs = new Dictionary<RoomType, List<RoomMetadata>>();
    private Dictionary<RoomType, float> _desiredRatios = new Dictionary<RoomType, float>();
    private Dictionary<RoomType, float> _currentContributions = new Dictionary<RoomType, float>();
    private bool _initialized = false;

    public void InitSeed(int seed)
    {
        if (_initialized == false)
        {
            Random.InitState(seed);
            SetupDict();
            InitializeContributions();
            _initialized = true;
        }
    }

    private void SetupDict()
    {
        _roomPrefabs.Add(RoomType.Start, StartRooms);
        _roomPrefabs.Add(RoomType.Combat, CombatRooms);
        _roomPrefabs.Add(RoomType.Treasure, TreasureRooms);
        _roomPrefabs.Add(RoomType.Corridor, Corridors);
        _roomPrefabs.Add(RoomType.Oddity, OddityRooms);

        _desiredRatios.Add(RoomType.Start, 1f);
        _desiredRatios.Add(RoomType.Combat, CombatRatio);
        _desiredRatios.Add(RoomType.Treasure, TreasureRatio);
        _desiredRatios.Add(RoomType.Corridor, CorridorRatio);
        _desiredRatios.Add(RoomType.Oddity, OddityRatio);
    }

    private void InitializeContributions()
    {
        foreach (RoomType type in System.Enum.GetValues(typeof(RoomType)))
        {
            _currentContributions[type] = 0;
        }
    }

    public RoomType SelectNextRoomType()
    {
        // Calculate the total of desired ratios
        float desiredTotal = 0;
        foreach (var ratio in _desiredRatios.Values)
        {
            desiredTotal += ratio;
        }

        // Calculate the current total contribution
        float currentTotal = 0;
        foreach (var contribution in _currentContributions.Values)
        {
            currentTotal += contribution;
        }

        // Determine eligible room types
        List<RoomType> eligibleTypes = new List<RoomType>();
        foreach (var roomType in _desiredRatios.Keys)
        {
            // Skip Start type since it is only generated first
            if (roomType == RoomType.Start) continue;

            float currentRatio = _currentContributions[roomType] / currentTotal;
            float desiredRatio = _desiredRatios[roomType] / desiredTotal;

            if (currentRatio <= desiredRatio)
            {
                eligibleTypes.Add(roomType);
            }
        }

        // Handle no eligible types
        if (eligibleTypes.Count == 0)
        {
            Debug.LogError("No eligible room types available for selection!");
            return RoomType.Corridor; // Fallback to Corridor or any default
        }

        // Randomly select an eligible type
        int randomIndex = Random.Range(0, eligibleTypes.Count);
        return eligibleTypes[randomIndex];
    }

    public RoomMetadata GetRoomPrefabOfType(RoomType roomType)
    {
        if (_initialized == false) { Debug.LogError("Rooms dictionary not setup yet"); }
        List<RoomMetadata> roomTypePrefabs = _roomPrefabs[roomType];
        if (roomTypePrefabs.Count > 0)
        {
            float totalWeight = 0f;

            // Calculate total weight
            foreach (var prefabWeight in roomTypePrefabs)
            {
                totalWeight += prefabWeight.SelectWeight;
            }

            // Pick a random value within the total weight range
            float randomValue = Random.Range(0f, totalWeight);

            // Iterate and find the corresponding prefab
            float cumulativeWeight = 0f;
            foreach (var prefabWeight in roomTypePrefabs)
            {
                cumulativeWeight += prefabWeight.SelectWeight;
                if (randomValue <= cumulativeWeight)
                {
                    return prefabWeight;
                }
            }
        }

        Debug.LogWarning($"No prefabs available for room type: {roomType}");
        return null;
    }

    public void AddRoomContribution(RoomType roomType, float contribution)
    {
        if (_currentContributions.ContainsKey(roomType))
        {
            _currentContributions[roomType] += contribution;
        }
        else
        {
            Debug.LogError($"Room type {roomType} does not exist in current contributions.");
        }
    }

    public void AddRoomContribution(RoomMetadata roomMetadata)
    {
        if (_currentContributions.ContainsKey(roomMetadata.Type))
        {
            _currentContributions[roomMetadata.Type] += roomMetadata.RatioContribution;
        }
        else
        {
            Debug.LogError($"Room type {roomMetadata.Type} does not exist in current contributions.");
        }
    }
}
