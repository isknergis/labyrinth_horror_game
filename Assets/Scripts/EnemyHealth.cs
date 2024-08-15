using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth = 100f;
    private bool isDead = false;  // isDead alanýný ekleyin

    private EnemyAI enemy; // Referans için private olarak deðiþtirildi

    public bool isDamage = false;

    PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        enemy = GetComponent<EnemyAI>();
    }

    void Update()
    {
        if (enemyHealth <= 0 && !isDead)
        {
            isDamage = false;
            Dead();
        }
    }

    public void ReduceHealth(float reduceHealth)
    {
        if (isDead) return;

        enemyHealth -= reduceHealth;

        if (enemyHealth > 0)
        {
            isDamage = true;

            // Geri tepme yönünü hesapla ve Hurt metoduna ilet
            Vector3 knockbackDirection = (transform.position - playerHealth.transform.position).normalized;
            enemy.Hurt(knockbackDirection);
        }
        else
        {
            isDamage = false;
            Dead();
        }
    }

    void Dead()
    {
        isDead = true;  // isDead alanýný burada kullanýyoruz
        enemy.DeadAnim();
        Debug.Log("Enemy Died!");
        isDamage = false;
        Destroy(gameObject, 3f);
    }

    public bool IsDead()  // isDead'in durumunu kontrol eden bir metod
    {
        return isDead;
    }
}