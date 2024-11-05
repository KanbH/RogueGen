using UnityEngine;

[CreateAssetMenu(fileName = "NewConsumable", menuName = "Item/Consumable")]
public class Consumable : Item
{
    // Fields for consumable-specific properties
    [SerializeField] private int effectStrength;
    [SerializeField] private float duration;  // Optional, if the effect is temporary
    [SerializeField] private ConsumableType consumableType;

    // Enum to define the type of consumable
    public enum ConsumableType
    {
        Health,
        Mana,
        Stamina,
        // Add more types as needed
    }

    // Method to use the consumable
    public void Use(CharacterStats characterStats)
    {
        /*
        switch (consumableType)
        {
            case ConsumableType.Health:
                characterStats.RestoreHealth(effectStrength);
                break;
            case ConsumableType.Mana:
                characterStats.RestoreMana(effectStrength);
                break;
            case ConsumableType.Stamina:
                characterStats.RestoreStamina(effectStrength);
                break;
            default:
                Debug.LogWarning("Undefined consumable type.");
                break;
        }
        */
        // Additional logic if duration is needed (e.g., for temporary buffs)
        if (duration > 0)
        {
            ApplyTimedEffect(characterStats);
        }
    }

    private void ApplyTimedEffect(CharacterStats characterStats)
    {
        // Apply any effects that last over time here, like buffs or debuffs
        // This could involve starting a coroutine or timer depending on your setup
    }
}
