using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class WeaponController : MonoBehaviour
{
    public float swingSpeed = 360f; // degrees per second
    public float arcAngle = 90f;    // total arc angle (swing width)
    public float weaponRange = 1.5f; // distance from player
    private float currentAngle = 0f;
    private float startingAngle = 0f;
    [SerializeField] private float knockbackMagnitude = 200f;
    private Vector3 startPosition;

    private bool isSwinging = false;

    public void StartSwing(Vector2 swingDirection)
    {
        startingAngle = (Mathf.Atan2(swingDirection.y, swingDirection.x) * Mathf.Rad2Deg - arcAngle/2);
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
            if (currentAngle >= startingAngle+arcAngle)
            {
                EndSwing();
            }
            
        }
    }

    private void EndSwing()
    {
        isSwinging = false;
        // Optionally, destroy the weapon object here or make it inactive
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PlayerController playerController = gameObject.GetComponentInParent<PlayerController>();
            playerController.DealDamage(collision.gameObject);
            playerController.DealKnockback(collision.gameObject, knockbackMagnitude);
        }
    }
}