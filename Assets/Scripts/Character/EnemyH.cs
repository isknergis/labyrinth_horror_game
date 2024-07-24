using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyH : Character
{
    EnemyAI enemyAI;

    protected override void Start()
    {
        base.Start();
        enemyAI = GetComponent<EnemyAI>();
    }

    protected override void Die()
    {
        base.Die();
        
        if (enemyAI != null)
        {
            enemyAI.DeadAnim();
        }
        
    }
}
