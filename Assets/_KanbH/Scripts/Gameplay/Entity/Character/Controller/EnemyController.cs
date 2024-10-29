using CharlieMadeAThing.NeatoTags.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EntityController
{
    [SerializeField] private float contactKnockbackMagnitude = 1000f;

    private CharacterStats _characterStats;
    private CharacterMovement _characterMovement;
    private MoveToDestination _moveToDestination;

    void Awake()
    {
        _characterStats = GetComponent<CharacterStats>();
        _characterMovement = GetComponent<CharacterMovement>();
        _moveToDestination = GetComponent<MoveToDestination>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.HasTag("Player"))
        {
            DealDamage(collision.gameObject);
            DealKnockback(collision.gameObject, contactKnockbackMagnitude);
        }
    }

    public override void TakeDamage(float damage)
    {
        _characterStats.Health -= damage;

        if (_characterStats.HealthBelow0)
        {
            Die();
        }
    }

    public override void Die()
    {
        this.gameObject.SetActive(false);
    }

    public override void DealDamage(GameObject target)
    {
        // Get the PlayerStats component on the player
        EntityController playerController = target.GetComponent<EntityController>();

        if (playerController != null)
        {
            // Deal damage to the player
            playerController.TakeDamage(DamageCalculator.CalculateDamage(_characterStats, playerController.GetCharacterStats()));
            Debug.Log("Enemy dealt damage to the player!");
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

    public override CharacterStats GetCharacterStats()
    {
        return _characterStats;
    }

}
