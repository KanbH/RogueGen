using UnityEngine;
using UnityEngine.Events;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _health;
    [SerializeField] private float _strength;
    [SerializeField] private float _toughness;
    [SerializeField] private float _agility;

    // Event to notify listeners about health changes
    public UnityEvent<float> OnHealthChanged = new UnityEvent<float>();

    public float MaxHealth => _maxHealth;

    public float Health
    {
        get { return _health; }
        set
        {
            Debug.Log("health changed");
            _health = Mathf.Clamp(value, 0, _maxHealth);
            OnHealthChanged.Invoke(_health / _maxHealth);
        }
    }

    public float Strength
    {
        get { return _strength; }
        private set
        {
            _strength = value;
        }
    }

    public float Toughness
    {
        get { return _toughness; }
        private set
        {
            _toughness = value;
        }
    }

    public float Agility
    {
        get { return _agility; }
        private set
        {
            _agility = value;
        }
    }

    public bool HealthAbove0 => Health > 0;
    public bool HealthBelow0 => !HealthAbove0;

}
