using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    [SerializeField] public GameObject weaponPrefab;

    private PlayerMovement _playerMovement;
    private CharacterStats _characterStats;

    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _characterStats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button or any attack key
        {
            Vector2 swingDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

            // Spawn and swing weapon
            SwingWeapon(swingDirection);
        }
    }

    void SwingWeapon(Vector2 swingDirection)
    {
        // Instantiate the weapon prefab
        GameObject weaponInstance = Instantiate(weaponPrefab, this.transform);

        // Get the WeaponController script and start the swing
        WeaponController weaponController = weaponInstance.GetComponent<WeaponController>();
        weaponController.StartSwing(swingDirection);
    }

    public override void DealDamage(GameObject target)
    {
        // Get the PlayerStats component on the player
        EntityController enemyController = target.GetComponent<EntityController>();

        if (enemyController != null)
        {
            // Deal damage to the player
            enemyController.TakeDamage(_characterStats.Strength);
            Debug.Log("Player dealt damage to the enemy: " + _characterStats.Strength);
        }
    }

    public override void TakeDamage(float damage)
    {
        //bla bla bla defense calculation
        _characterStats.Health -= damage;
        Debug.Log($"Player took {damage} damage and have {_characterStats.Health} HP left");

        if (_characterStats.IsDead)
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

}
