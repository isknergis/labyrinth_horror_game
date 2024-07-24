using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIForEnemy : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    Transform target;
    public bool isDead = false;

    bool isHurt = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isDead) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > 2 && distance < 10)
        {
            agent.updatePosition = true;
            if (agent.isOnNavMesh)
            {
                agent.SetDestination(target.position);
            }
            anim.SetBool("isWalking", true);
        }
        else if (distance <= 2)
        {
            agent.updatePosition = false;
            anim.SetBool("isWalking", false);
        }
    }

    public void DeadAnim()
    {
        isDead = true;
        anim.SetTrigger("Dead");
        Debug.Log("Dead animation triggered");
    }

    public void Hurt()
    {
        agent.enabled = false;
        anim.SetTrigger("Hurt");
        Debug.Log("Hurt animation triggered");
        StartCoroutine(EnableNavAgentAfterDelay(0.75f));
    }

    IEnumerator EnableNavAgentAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        agent.enabled = true;
        agent.SetDestination(target.position);
    }
}