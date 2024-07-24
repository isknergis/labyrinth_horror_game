using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerH : Character
{
    protected override void Die()
    {
        base.Die();
       
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth += amount;
        
    }
}
