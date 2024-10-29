using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _strength;
    [SerializeField] private float _toughness;
    [SerializeField] private float _agility;

    private void Awake()
    {
        
    }

    public float Health
    {
        get { return _health; }
        set
        {
            _health = Mathf.Max(0, value);  // Ensure health never drops below 0
        }
    }

    public float Strength
    {
        get { return _strength; }
        private set
        {
            _strength = Mathf.Max(0, value);  // Ensure health never drops below 0
        }
    }

    public float Toughness
    {
        get { return _toughness; }
        private set
        {
            _toughness = Mathf.Max(0, value);  // Ensure health never drops below 0
        }
    }

    public float Agility
    {
        get { return _agility; }
        private set
        {
            _agility = Mathf.Max(0, value);  // Ensure health never drops below 0
        }
    }

    public bool HealthAbove0 => Health > 0;
    public bool HealthBelow0 => !HealthAbove0;


}
