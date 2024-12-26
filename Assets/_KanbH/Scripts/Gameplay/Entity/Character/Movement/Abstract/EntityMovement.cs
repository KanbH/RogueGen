using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityMovement : MonoBehaviour
{
    [SerializeField] protected bool _isKnockbackImmune = false;

    protected abstract void HandleMovement();
    protected abstract void UpdateMovementSpeed();
    public abstract Vector2 GetMovement();
    public abstract void ReceiveKnockback(Vector2 knockbackDirection, float magnitude);
}
