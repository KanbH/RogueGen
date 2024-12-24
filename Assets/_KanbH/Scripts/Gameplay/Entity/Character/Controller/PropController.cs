using UnityEngine;
using NUnit.Framework;

public class PropController : EntityController
{
    private Inventory _inventory;
    void Awake()
    {
        _characterStats = GetComponent<CharacterStats>();
        _inventory = GetComponent<Inventory>();

        Assert.IsNotNull(_faction);
        Assert.IsNotNull(_characterStats);
        Assert.IsNotNull(_inventory);
    }

    public override void AttackAtDirection(Vector2 AttackDirection)
    {
        
    }

    public override void Die()
    {
        _inventory.DropAllItems();
        this.gameObject.SetActive(false);
    }

    public override void DealKnockback(GameObject target, float magnitude)
    {
        Vector2 knockbackDirection = target.transform.position - transform.position;
        EntityMovement entityMovement = target.GetComponent<EntityMovement>();
        if (entityMovement != null)
        {
            entityMovement.ReceiveKnockback(knockbackDirection, magnitude);
        }
    }

    public override void DealDamage(GameObject target)
    {
        EntityController entityController = target.GetComponent<EntityController>();

        if (entityController != null)
        {
            // Deal damage to the player
            entityController.TakeDamage(DamageCalculator.CalculateDamage(_characterStats, entityController.GetCharacterStats()));
            Debug.Log("Enemy dealt damage to the player!");
        }
    }

    public override void TakeDamage(float damage)
    {
        if (!_isInvincible)
        {
            _characterStats.Health -= damage;

            if (_characterStats.HealthBelow0)
            {
                Die();
            }
        }
    }
}
