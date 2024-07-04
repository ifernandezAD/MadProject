using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerAnt : MonoBehaviour
{

    public static Action onResourceCollected;

    [SerializeField] private GameObject targetResource;
    private Transform storageArea;
    Transform resourcesContainer;
    private NavMeshAgent agent;
    private Animator animator;


    void Awake()
    {
        storageArea = GameManager.instance.storageArea;
        resourcesContainer = GameManager.instance.resourcesContainer;
        agent = GetComponent<NavMeshAgent>();
        animator=GetComponent<Animator>();
    }

    void Update()
    {
        if (targetResource == null)
        {
            FindNewResource();
        }

        if (targetResource != null)
        {
            agent.SetDestination(targetResource.transform.position);
        }

        UpdateAnimation();
    }

    void UpdateAnimation()
    {
       if (agent.velocity.magnitude > 0.1f)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    void FindNewResource()
    {
        if (resourcesContainer != null)
        {
            int childCount = resourcesContainer.childCount;
            if (childCount > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, childCount);
                targetResource = resourcesContainer.GetChild(randomIndex).gameObject;
            }
            else
            {
                agent.SetDestination(transform.position);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Resource"))
        {
            if (targetResource != null && other.gameObject == targetResource)
            {
                onResourceCollected?.Invoke();
                Destroy(targetResource);
                targetResource = null;  
            }
        }
    }
}
