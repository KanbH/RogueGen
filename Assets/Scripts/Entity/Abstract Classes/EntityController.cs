using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityController : MonoBehaviour
{
    public abstract void DealDamage(GameObject target);
    public abstract void TakeDamage(float damage);
    public abstract void DealKnockback(GameObject target, float magnitude);
    public abstract void Die();
}
