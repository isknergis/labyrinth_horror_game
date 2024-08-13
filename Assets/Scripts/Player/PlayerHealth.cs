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

    public static PlayerHealth PH;

    public bool isDead = false;

    Transform target;

    [SerializeField] public float increaseHealth = 20f;
    [SerializeField] public float damageAmount = 10f;
    [SerializeField] public float damageInterval = 5f;

    public AudioSource AS;

    public AudioClip attackSound;

    public Image healthbar;
    public Text health;

    public static PlayerHealth instance;

    public bool isDamage; 

 
    void Start()
    {
        isDamage = false; 
        isDead = false;

      
        AS = GetComponent<AudioSource>();

        currentHealth = maxHealth;

        ImageHealthBar();


        target = GameObject.FindGameObjectWithTag("Enemy")?.transform;
        if (target == null)
        {
            Debug.LogError("Enemy tag'li bir obje bulunamadý.");
        }

        InvokeRepeating("CheckAndDamagePlayer", 0f, damageInterval);
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            isDamage = false;
            currentHealth = 0;
            Dead();
        }
    }

    void ImageHealthBar()
    {
        healthbar.fillAmount = currentHealth / maxHealth;
        health.text = currentHealth.ToString();
    }

    void CheckAndDamagePlayer()
    {
        if (target == null) return;

    float distance = Vector3.Distance(transform.position, target.position);
    if (distance <= 5)
    {
        if (isDamage) return; 

        DamagePlayer(damageAmount);
        AS.PlayOneShot(attackSound);
        isDamage = false; 
    }
    else
    {
        isDamage = false; 
    }
    }

    public void DamagePlayer(float damage)
    {
        isDamage = true; 
        currentHealth -= damage;
        ImageHealthBar();
        if (currentHealth <= 0)
        {
            isDamage = false;
            Dead();
        }
    }

    void Dead()
    {
        currentHealth = 0;
        isDead = true;
        
        Debug.Log("Öldü");
        CancelInvoke("CheckAndDamagePlayer");
        isDamage = false;
    }

    public void IncreaseHealth()
    {
        currentHealth += increaseHealth;
        currentHealth = Mathf.Min(currentHealth, maxHealth); 
        Debug.Log("Saðlýk arttýrýldý");
        ImageHealthBar();
    }
}
