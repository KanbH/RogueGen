using UnityEngine;

public static class DamageCalculator
{
    public static float CalculateDamage(CharacterStats attackerStats, CharacterStats defenderStats)
    {
        float damage = attackerStats.Strength * (50 / (50 + defenderStats.Toughness));
        Debug.Log(damage + "damage has been dealt");
        return damage;
    }
}
