using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityController : MonoBehaviour
{
    [SerializeField] protected Faction _faction;
    [SerializeField] protected bool _isInvincible = false;
    protected CharacterStats _characterStats;

    public abstract void AttackAtDirection(Vector2 direction);
    public abstract void DealDamage(GameObject target);
    public abstract void TakeDamage(float damage);
    public abstract void DealKnockback(GameObject target, float magnitude);
    public abstract void Die();

    public bool IsSameFaction(GameObject target)
    {
        EntityController targetController = target.GetComponent<EntityController>();
        if (targetController != null)
        {
            if (GetFactionID() == 1000) { return false; }
            if (GetFactionID() == targetController.GetFactionID()) { return true; }
        }
        return false;
    }

    public int GetFactionID()
    {
        return this._faction.FactionID;
    }

    public CharacterStats GetCharacterStats()
    {
        return this._characterStats;
    }
}
