using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMovementStatsSO", menuName = "Stats/Movement Stats")]
public class MovementStatsSO : ScriptableObject
{
    [SerializeField] private float baseMoveSpeed = 4500f; // 4500f
    [SerializeField] private float maxMoveSpeed = 9000f; // 9000f
    [SerializeField] private float decayRate = 0.05f; // 0.05f
    [SerializeField] private float knockbackResistance = 1f; //1f

    public float BaseMoveSpeed { get => baseMoveSpeed;}

    public float MaxMoveSpeed { get => maxMoveSpeed;}

    public float DecayRate { get => decayRate;}

    public float KnockbackResistance { get => knockbackResistance;}
}
