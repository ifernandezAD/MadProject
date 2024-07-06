using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CopAnt : MonoBehaviour
{
    private Transform zombieAntContainer;
    private NavMeshAgent agent;
    private Animator animator;
    private Rigidbody rb;
    private GameObject targetZombieAnt;

    void Awake()
    {
        zombieAntContainer = GameManager.instance.zombiesAntContainer;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb=GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (targetZombieAnt == null)
        {
            FindNewZombieAnt();
        }

        if (targetZombieAnt != null)
        {
            agent.SetDestination(targetZombieAnt.transform.position);
        }

        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        animator.SetBool("isWalking", agent.velocity.magnitude > 0.1f);
    }

    void FindNewZombieAnt()
    {
        if (zombieAntContainer != null && zombieAntContainer.childCount > 0)
        {
            rb.isKinematic=false;
            int randomIndex = Random.Range(0, zombieAntContainer.childCount);
            targetZombieAnt = zombieAntContainer.GetChild(randomIndex).gameObject;
        }
        else
        {
            agent.SetDestination(transform.position);
            rb.isKinematic=true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == targetZombieAnt)
        {
            DestroyZombieAnt();
        }
    }

    void DestroyZombieAnt()
    {
        Destroy(targetZombieAnt);
        Destroy(gameObject);
        targetZombieAnt = null;
    }
}
