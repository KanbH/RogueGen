using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapSizeSO", menuName = "Map/Map Size")]
public class MapSizeSO : ScriptableObject
{
    [SerializeField] private int leftWorldBound, rightWorldBound, bottomWorldBound, topWorldBound;

    public int LeftWorldBound { get => leftWorldBound; }

    public int RightWorldBound { get => rightWorldBound; }

    public int BottomWorldBound { get => bottomWorldBound; }

    public int TopWorldBound { get => topWorldBound; }
}
