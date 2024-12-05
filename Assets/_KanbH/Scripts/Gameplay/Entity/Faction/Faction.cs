using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewFaction", menuName = "Factions/Faction", order = 1)]
public class Faction : ScriptableObject
{
    [Tooltip("Name of the faction")]
    public string FactionName;

    [Tooltip("Attitudes with other factions. Key: Faction Name, Value: Hostility Level (-1 to 1)")]
    public List<FactionRelationship> Relationships;

    [Tooltip("Default attitude toward other faction if not specified in Relationships")]
    public float DefaultAttitudeValue = 0;

    // Get the hostility level towards another faction
    public float GetRelationship(string otherFaction)
    {
        foreach (var relation in Relationships)
        {
            if (relation.factionName == otherFaction)
                return Mathf.Clamp(relation.hostilityLevel, -1f, 1f);
        }
        return 0f; // Default neutral if no relationship is defined
    }

    public void SetRelationship(string otherFaction, float hostilityLevel)
    {
        foreach (var relation in Relationships)
        {
            if (relation.factionName == otherFaction)
            {
                relation.hostilityLevel = Mathf.Clamp(hostilityLevel, -1f, 1f);
                return;
            }
        }
        Relationships.Add(new FactionRelationship { factionName = otherFaction, hostilityLevel = hostilityLevel });
    }
}

[System.Serializable]
public class FactionRelationship
{
    public string factionName;
    public float hostilityLevel;
}