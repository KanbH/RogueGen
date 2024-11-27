using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : EntityMovement
{
    [SerializeField] private MovementStatsSO movementStatsSO;

    private CharacterStats _characterStats;
    private Rigidbody2D _rigidbody2d;

    private Vector2 _movement;
    private float _movementSpeed;

    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _characterStats = GetComponent<CharacterStats>();
        _movement = new Vector2(0, 0);
    }

    void Update()
    {
        //To implement: Input System
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        _movement = new Vector2(moveX, moveY);
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    protected override void HandleMovement()
    {
        UpdateMovementSpeed();
        // Apply force-based movement
        //Debug.Log("Player's movespeed is " + _movementSpeed);
        _rigidbody2d.AddForce(_movement.normalized * _movementSpeed * Time.fixedDeltaTime, ForceMode2D.Force);

    }

    protected override void UpdateMovementSpeed()
    {
        // Diminishing returns formula for agility affecting movement speed
        _movementSpeed = movementStatsSO.BaseMoveSpeed + (movementStatsSO.MaxMoveSpeed - movementStatsSO.BaseMoveSpeed) * (1 - Mathf.Exp(-_characterStats.Agility * movementStatsSO.DecayRate));
    }

    public override void ReceiveKnockback(Vector2 knockbackDirection, float magnitude)
    {
        float calculatedMagnitude = magnitude / movementStatsSO.KnockbackResistance;
        Debug.Log("Player has taken knockback");
        _rigidbody2d.AddForce(knockbackDirection.normalized * calculatedMagnitude, ForceMode2D.Impulse);
    }

    public Vector2 GetMovement()
    {
        return _movement;
    }
}
