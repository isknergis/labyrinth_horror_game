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
    [SerializeField] public float damageAmount = 10f; // Hasar miktarý
    [SerializeField] public float damageInterval = 5f; // Hasar verme aralýðý (saniye)

    AudioSource AS;

    public AudioClip attackSound;


    public Image healthbar;
    public Text health;


    public static PlayerHealth instance;

    //void Awake()
    //{

    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject); // Bu nesneyi yok etme
    //    }
    //    else if (instance != this)
    //    {
    //        Destroy(gameObject);
    //    }
    //    PH = this;
    //}

    void Start()
    {
        AS = GetComponent<AudioSource>();

        currentHealth = maxHealth;

        ImageHealthBar();

        target = GameObject.FindGameObjectWithTag("Enemy").transform;

        // Belirli aralýklarla hasar verme iþlemi baþlatýlýyor
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


    void ImageHealthBar()
    {
        healthbar.fillAmount = currentHealth / maxHealth;
        health.text = currentHealth.ToString();

    }

    void CheckAndDamagePlayer()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= 3)
        {
            DamagePlayer(damageAmount);
          
            AS.PlayOneShot(attackSound);
        }

       
    }

    public void DamagePlayer(float damage)
    {
        currentHealth -= damage;
        ImageHealthBar();
        if (currentHealth <= 0)
        {
            Dead();
        }

    }

    void Dead()
    {
        currentHealth = 0;
        isDead = true;
        Debug.Log("Öldü");
        CancelInvoke("CheckAndDamagePlayer");
    }



    public void IncreaseHealth()
    {
        currentHealth += increaseHealth;
       // currentHealth = Mathf.Min(currentHealth, maxHealth);
        ImageHealthBar();
    }
}
