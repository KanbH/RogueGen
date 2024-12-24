using CharlieMadeAThing.NeatoTags.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class EnemyController : EntityController
{
    private CharacterMovement _characterMovement;
    private MoveToDestination _moveToDestination;
    private EquipmentHandler _equipmentHandler;

    void Awake()
    {
        _characterStats = GetComponent<CharacterStats>();
        _characterMovement = GetComponent<CharacterMovement>();
        _moveToDestination = GetComponent<MoveToDestination>();
        _equipmentHandler = GetComponent<EquipmentHandler>();

        Assert.IsNotNull(_faction);
        Assert.IsNotNull(_characterStats);
        Assert.IsNotNull(_characterMovement);
        Assert.IsNotNull(_moveToDestination);
        Assert.IsNotNull(_equipmentHandler);
    }

    public override void AttackAtDirection(Vector2 AttackDirection)
    {
        _equipmentHandler.PerformAttack(AttackDirection, transform.position);
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

    public override void DealKnockback(GameObject target, float magnitude)
    {
        Vector2 knockbackDirection = target.transform.position - transform.position;
        EntityMovement entityMovement = target.GetComponent<EntityMovement>();
        if (entityMovement != null)
        {
            entityMovement.ReceiveKnockback(knockbackDirection, magnitude);
        }
    }

    public override void Die()
    {
        this.gameObject.SetActive(false);
    }
}
