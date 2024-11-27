using CharlieMadeAThing.NeatoTags.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class WeaponBatController : WeaponController
{
    public float swingSpeed = 400f; // degrees per second
    public float arcAngle = 180f;    // total arc angle (swing width)
    private float currentAngle = 0f;
    private float startingAngle = 0f;
    [SerializeField] private float knockbackMagnitude = 40f;

    private bool isSwinging = false;

    public override void UseWeapon(Vector2 attackDirection, Vector2 attackerPosition)
    {
        if (isSwinging == false)
        {
            StartSwing((attackDirection - attackerPosition).normalized);
        }
    }
    public void StartSwing(Vector2 swingDirection)
    {
        startingAngle = (Mathf.Atan2(swingDirection.y, swingDirection.x) * Mathf.Rad2Deg - arcAngle / 2);
        // Calculate the starting angle based on the direction (e.g., mouse position)
        currentAngle = startingAngle;
        // Reset weapon's local rotation
        transform.localRotation = Quaternion.Euler(0, 0, startingAngle);

        isSwinging = true;
    }

    void Update()
    {
        if (isSwinging)
        {
            // Rotate the weapon along the arc
            float rotationStep = swingSpeed * Time.deltaTime;
            currentAngle += rotationStep;
            transform.localRotation = Quaternion.Euler(0, 0, currentAngle);

            // Stop the swing once it completes the full arc
            if (currentAngle >= startingAngle + arcAngle)
            {
                EndSwing();
            }

        }
    }

    private void EndSwing()
    {
        isSwinging = false;
        // Optionally, destroy the weapon object here or make it inactive
        this.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.HasTag("Enemy"))
        {
            PlayerController playerController = gameObject.GetComponentInParent<PlayerController>();
            playerController.DealDamage(collision.gameObject);
            playerController.DealKnockback(collision.gameObject, knockbackMagnitude);
        }
    }
}