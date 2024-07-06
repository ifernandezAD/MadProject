using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAnt : MonoBehaviour
{
    public static Action onNoWorkerAntsLeft;
    private NavMeshAgent agent;
    private Animator animator;
    private Rigidbody rb;
    private Transform workerAntContainer; 

    private GameObject targetWorkerAnt;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb=GetComponent<Rigidbody>();
        workerAntContainer = GameManager.instance.workAntsContainer;
    }

    void Update()
    {
        if (targetWorkerAnt == null)
        {
            FindNewWorkerAnt();
        }

        if (targetWorkerAnt != null)
        {
            agent.SetDestination(targetWorkerAnt.transform.position);
        }

        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        animator.SetBool("isWalking", agent.velocity.magnitude > 0.1f);
    }

    void FindNewWorkerAnt()
    {
        WorkerAnt[] workerAnts = FindObjectsOfType<WorkerAnt>();

        if (workerAnts.Length > 0)
        {
            rb.isKinematic=false;
            int randomIndex = UnityEngine.Random.Range(0, workerAnts.Length);
            targetWorkerAnt = workerAnts[randomIndex].gameObject;
        }
        else
        {
            agent.SetDestination(transform.position);  
            rb.isKinematic=true;
            onNoWorkerAntsLeft?.Invoke();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("WorkerAnt"))
        {
            if (targetWorkerAnt != null && other.gameObject == targetWorkerAnt)
            {
                InfectWorkerAnt(other.gameObject);
            }
        }
    }

    void InfectWorkerAnt(GameObject workerAnt)
    {


        Destroy(workerAnt);

        GameObject newZombieAnt = Instantiate(gameObject, workerAnt.transform.position, Quaternion.identity);
        
        if (GameManager.instance.zombiesAntContainer != null)
        {
            newZombieAnt.transform.parent = GameManager.instance.zombiesAntContainer;
        }
    }
}
