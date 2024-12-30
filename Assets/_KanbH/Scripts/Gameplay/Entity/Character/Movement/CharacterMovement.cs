using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : EntityMovement
{
    [SerializeField] private MovementStatsSO movementStatsSO;

    private CharacterStats _characterStats;
    private Rigidbody2D _rigidbody2d;

    private Vector2 _movementDirection = Vector2.zero;
    private float _movementSpeed;

    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _characterStats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        // AI movement logic would go here, for example:
        // _movement = GetDirectionBasedOnAI(); // Implement your AI logic to set the movement direction.
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    public override void SetMovementDirection(Vector2 direction)
    {
        _movementDirection = direction;
    }

    protected override void HandleMovement()
    {
        UpdateMovementSpeed();
        // Apply force-based movement
        //Debug.Log("AI's movespeed is " + _movementSpeed);
        _rigidbody2d.AddForce(_movementDirection.normalized * _movementSpeed * Time.fixedDeltaTime);
    }

    protected override void UpdateMovementSpeed()
    {
        // Same diminishing returns formula for movement speed based on agility
        _movementSpeed = movementStatsSO.BaseMoveSpeed + (movementStatsSO.MaxMoveSpeed - movementStatsSO.BaseMoveSpeed) * (1 - Mathf.Exp(-_characterStats.Agility * movementStatsSO.DecayRate));
    }

    public override void ReceiveKnockback(Vector2 knockbackDirection, float magnitude)
    {
        if (!_isKnockbackImmune)
        {
            float calculatedMagnitude = magnitude / movementStatsSO.KnockbackResistance;
            Debug.Log("AI has taken knockback");
            _rigidbody2d.AddForce(knockbackDirection.normalized * calculatedMagnitude, ForceMode2D.Impulse);
        }
    }

    public override Vector2 GetMovement()
    {
        return _movementDirection;
    }
}
