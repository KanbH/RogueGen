using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityMovement : MonoBehaviour
{
    [SerializeField] protected bool _isKnockbackImmune = false;
    public abstract void SetMovementDirection(Vector2 direction);
    protected abstract void HandleMovement();
    protected abstract void UpdateMovementSpeed();
    public abstract Vector2 GetMovement();
    public abstract void ReceiveKnockback(Vector2 knockbackDirection, float magnitude);
}
