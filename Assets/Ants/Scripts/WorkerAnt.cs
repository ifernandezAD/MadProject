using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerAnt : MonoBehaviour
{
    Transform storageArea;
    Transform resourcesContainer;
    [SerializeField] private GameObject targetResource;
    private NavMeshAgent agent;

    void Awake()
    {
        storageArea = GameManager.instance.storageArea;
        resourcesContainer = GameManager.instance.resourcesContainer;
        agent = GetComponent<NavMeshAgent>();
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
    }

    void FindNewResource()
    {
        if (resourcesContainer != null)
        {
            int childCount = resourcesContainer.childCount;
            if (childCount > 0)
            {
                int randomIndex = Random.Range(0, childCount);
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
                Destroy(targetResource);
                targetResource = null;  
            }
        }
    }
}
