using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth = 100f;
    EnemyAI enemy;

    public bool isDamage = false; 

    PlayerHealth playerHealth;
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        enemy = GetComponent<EnemyAI>();
    }

    void Update()
    {
        
        if (enemyHealth <= 0 && !enemy.isDead)
        {
            isDamage = false;
            Dead();
        }
    }

    public void ReduceHealth(float reduceHealth)
    {
        if (enemy.isDead) return; 

        enemyHealth -= reduceHealth;

      
        if (enemyHealth > 0)
        {
            isDamage = true; 
            enemy.Hurt();
        }
        else
        {
          
            isDamage = false;
            enemy.DeadAnim();
            Dead();
        }
    }

  
    void Dead()
    {
        enemy.isDead = true;
        playerHealth.isDamage = false;
        Debug.Log("Enemy Died!");
        
        isDamage = false;
        Destroy(gameObject, 5f);
    }
}
