using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

using static UnityEngine.GraphicsBuffer;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;

    public static PlayerHealth PH;

    public bool isDead = false;

    Transform target;

    [SerializeField] public float increaseHealth = 20f;
    [SerializeField] public float damageAmount = 10f; // Hasar miktar�
    [SerializeField] public float damageInterval = 5f; // Hasar verme aral��� (saniye)

    void Awake()
    {
        PH = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        target = GameObject.FindGameObjectWithTag("Enemy").transform;

        // Belirli aral�klarla hasar verme i�lemi ba�lat�l�yor
        InvokeRepeating("CheckAndDamagePlayer", 0f, damageInterval);
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Dead();
        }
    }

    void CheckAndDamagePlayer()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= 3)
        {
            DamagePlayer(damageAmount);
        }
    }

    public void DamagePlayer(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        currentHealth = 0;
        isDead = true;
        Debug.Log("�ld�");
        CancelInvoke("CheckAndDamagePlayer"); // Karakter �ld���nde hasar verme i�lemini durdur
    }



    public void IncreaseHealth()
    {
        currentHealth += increaseHealth;
    }
}
