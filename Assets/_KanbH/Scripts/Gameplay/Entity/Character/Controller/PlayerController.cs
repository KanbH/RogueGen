using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class PlayerController : EntityController
{
    private PlayerMovement _playerMovement;
    private CharacterStats _characterStats;
    private InventoryManager _inventoryManager;

    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _characterStats = GetComponent<CharacterStats>();
        _inventoryManager = GetComponent<InventoryManager>();

        Assert.IsNotNull(_playerMovement);
        Assert.IsNotNull(_characterStats);
        Assert.IsNotNull(_inventoryManager);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the cooldown has passed before allowing a swing
        //if (Time.time >= lastSwingTime + _weaponSwingCooldown)
        {
            // Check for input to swing (you can replace this with a more complex input system later)
            DetectAttackInput();
        }
    }

    private void DetectAttackInput()
    {
        // Detect left mouse button press
        if (Input.GetMouseButtonDown(0)) // Left mouse button or any attack key
        {
            Vector2 AttackDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

            // Attempted to use weapon
            _inventoryManager.AttemptToUseWeapon(AttackDirection);
        }
    }
    /*
    void SwingWeapon(Vector2 swingDirection)
    {
        // Instantiate the weapon prefab
        GameObject weaponInstance = Instantiate(weaponPrefab, this.transform);

        // Get the WeaponController script and start the swing
        WeaponController weaponController = weaponInstance.GetComponent<WeaponController>();
        weaponController.StartSwing(swingDirection);
    }
    */

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
        //bla bla bla defense calculation
        _characterStats.Health -= damage;
        Debug.Log($"Player took {damage} damage and have {_characterStats.Health} HP left");

        if (_characterStats.HealthBelow0)
        {
            //blablabla resurection
            Die();
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
        //blablabla dead animation
        this.gameObject.SetActive(false);
        Debug.Log("Player has died");
    }

    public override CharacterStats GetCharacterStats()
    {
        return _characterStats;
    }
}
