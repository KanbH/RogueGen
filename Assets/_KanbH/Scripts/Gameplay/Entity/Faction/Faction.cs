using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewFaction", menuName = "Factions/Faction", order = 1)]
public class Faction : ScriptableObject
{
    [Tooltip("Integer ID of this faction")]
    public int FactionID;

    [Tooltip("Name of this faction")]
    public string FactionName;
}