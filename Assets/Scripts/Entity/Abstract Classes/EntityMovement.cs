using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityMovement : MonoBehaviour
{
    protected abstract void HandleMovement();
    protected abstract void UpdateMovementSpeed();

    public abstract void ReceiveKnockback(Vector2 knockbackDirection, float magnitude);
}
