using UnityEngine;

[CreateAssetMenu(fileName = "NewRoomMetadata", menuName = "Map/Room Metadata")]
public class RoomMetadata : ScriptableObject
{
    public GameObject Prefab;
    public string Name;
    public RoomType Type;
    public float Weight;
}

public enum RoomType
{
    Start,
    Combat,
    Treasure,
    Corridor,
    Oddity
}
