using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;
    public bool isDead = false;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        
    }

    protected virtual void Die()
    {
        isDead = true;
       
    }
}





