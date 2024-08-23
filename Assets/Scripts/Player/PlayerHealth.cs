using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

using static UnityEngine.GraphicsBuffer;

using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;
    public bool isDead = false;

    private AudioSource AS;
    public AudioClip attackSound;

    public Image healthbar;
    public Text health;

    private List<Transform> enemies;
    private bool isTakingDamage = false;

    void Start()
    {
        AS = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        UpdateHealthBar();

        enemies = new List<Transform>();
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(enemy.transform);
        }

        InvokeRepeating("CheckAndDamagePlayer", 0f, 1f);
    }

    void Update()
    {
        if (isDead) return;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    void UpdateHealthBar()
    {
        healthbar.fillAmount = currentHealth / maxHealth;
        health.text = currentHealth.ToString();
    }

    void CheckAndDamagePlayer()
    {
        if (isDead || isTakingDamage) return;

        foreach (Transform enemyTransform in enemies)
        {
            EnemyHealth enemyHealth = enemyTransform.GetComponent<EnemyHealth>();

            if (enemyHealth != null && !enemyHealth.IsDead())  // isDead kontrolünü yapýyoruz
            {
                float distance = Vector3.Distance(transform.position, enemyTransform.position);
                if (distance <= 3f)
                {
                    DamagePlayer(10f);
                    AS.PlayOneShot(attackSound);
                    break;
                }
            }
        }
    }



    public void DamagePlayer(float damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Player is dead.");
        CancelInvoke("CheckAndDamagePlayer");
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        UpdateHealthBar();
        Debug.Log("Health increased.");
    }
}
