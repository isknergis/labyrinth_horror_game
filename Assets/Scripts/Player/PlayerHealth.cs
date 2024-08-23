using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

using static UnityEngine.GraphicsBuffer;

using UnityEngine.UI;
using Unity.VisualScripting;


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

    public float distance = 4f;


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
        if(Input.GetKeyDown(KeyCode.X))
        {
            HealthBarCount();
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

    public void HealthBarCount()
    {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, distance); // shootPoint yerine transform.position kullanýn
        foreach (var collider in hitColliders)
        {
            HealthBar healthBar = collider.GetComponent<HealthBar>();
            if (healthBar != null)
            {
               healthBar.Collect(); // Ammo scriptindeki Collect() metodu çaðrýlýr
              IncreaseHealth();
                break; // Bir pil bulduktan sonra döngüyü durdurur
            }
        }
    }
    public void IncreaseHealth()
    {
        float amount = 20;
        currentHealth = amount+ currentHealth;
        UpdateHealthBar();
        Debug.Log("Health increased.");
    }
}
