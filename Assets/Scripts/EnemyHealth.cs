using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth = 100f;
    EnemyAI enemy;

    void Start()
    {
        enemy = GetComponent<EnemyAI>();
    }

    void Update()
    {
        if (enemyHealth <= 0 && !enemy.isDead)
        {
            Dead();
        }
    }

    public void ReduceHealth(float reduceHealth)
    {
        enemyHealth -= reduceHealth;

        if (enemyHealth > 0)
        {
            enemy.Hurt();
        }
        else if (!enemy.isDead)
        {
            enemy.DeadAnim();
            Dead();
        }
    }

    void Dead()
    {
        enemy.isDead = true;
        Debug.Log("Enemy Died!");
        Destroy(gameObject, 5f); // Düþmaný 5 saniye sonra yok et
    }
}
