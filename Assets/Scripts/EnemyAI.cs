using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private Transform target;
    public bool isDead = false;

    private AudioSource AS;
    public AudioClip hurtSound;

    void Start()
    {
        AS = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        Debug.Log("Baþlangýç Pozisyonu: " + transform.position);
    }

    void Update()
    {
        if (isDead) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > 3 && distance < 10)
        {
            if (agent.isOnNavMesh)
            {
                agent.updatePosition = true;
                agent.SetDestination(target.position);
                anim.SetBool("isWalking", true);
            }
            else
            {
                Debug.LogWarning("Enemy is not on NavMesh!");
            }
        }
        else
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
        anim.SetBool("isWalking", false); // Yürüme animasyonunu durdur
        AS.PlayOneShot(hurtSound);

        Debug.Log("Hurt animation triggered");
        StartCoroutine(EnableNavAgentAfterDelay(0.75f));
    }

    IEnumerator EnableNavAgentAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        agent.enabled = true;

        // Hurt animasyonu sonrasý yürüme animasyonunu baþlatma
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > 3 && distance < 10)
        {
            agent.SetDestination(target.position);
            anim.SetBool("isWalking", true);
        }
    }
}
