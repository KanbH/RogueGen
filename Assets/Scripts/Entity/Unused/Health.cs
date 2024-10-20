using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    private float health;

    public Health(float health)
    {
        this.health = health;
    }

    public void ReduceHealth(float damage)
    {
        this.health -= damage;
    }

    public void HealHealth(float healAmount)
    {
        this.health += healAmount;
    }

    public float GetHealth()
    {
        return this.health;
    }

    public bool IsAlive()
    {
        return this.health > 0;
    }

    public bool IsDead()
    {
        return this.health <= 0;
    }
}
