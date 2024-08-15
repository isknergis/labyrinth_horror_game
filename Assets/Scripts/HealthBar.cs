using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : Collectable
{
    [SerializeField]  private PlayerHealth playerHealth;
    void Start()
    {
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
    }

    public override void Collect()
    {
        base.Collect();
        if (playerHealth)
        {
            float healAmount = 20f; // Artýþ miktarýný burada belirleyin.
            playerHealth.IncreaseHealth(healAmount);
        }


    }

}
