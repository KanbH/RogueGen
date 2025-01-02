using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class PlayerController : EntityController
{

    private CharacterMovement _characterMovement;
    private InventoryManager _inventoryManager;
    private EquipmentHandler _equipmentHandler;

    //private AudioSource _audioSource;
    void Awake()
    {
        _characterStats = GetComponent<CharacterStats>();
        _characterMovement = GetComponent<CharacterMovement>();
        _inventoryManager = GetComponent<InventoryManager>();
        _equipmentHandler = GetComponent<EquipmentHandler>();

        //_audioSource = GetComponent<AudioSource>();

        Assert.IsNotNull(_faction);
        Assert.IsNotNull(_characterStats);
        Assert.IsNotNull(_characterMovement);
        Assert.IsNotNull(_inventoryManager);
        Assert.IsNotNull(_equipmentHandler);
    }

    public override void AttackAtDirection(Vector2 AttackDirection)
    {
        _equipmentHandler.PerformAttack(AttackDirection, transform.position);
    }

    public override void DealDamage(GameObject target)
    {
        // Get the PlayerStats component on the player
        EntityController enemyController = target.GetComponent<EntityController>();

        if (enemyController != null)
        {
            // Deal damage to the player
            enemyController.TakeDamage(DamageCalculator.CalculateDamage(_characterStats, enemyController.GetCharacterStats()));
            Debug.Log("Player dealt damage to the enemy!");
        }
    }

    public override void TakeDamage(float damage)
    {
        if (!_isInvincible)
        {
            //_audioSource.Play();
            //bla bla bla defense calculation
            _characterStats.Health -= damage;
            Debug.Log($"Player took {damage} damage and have {_characterStats.Health} HP left");

            if (_characterStats.HealthBelow0)
            {
                //blablabla resurection
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
        Debug.Log("Player has died");
    }

}
