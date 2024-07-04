using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerAnt : MonoBehaviour
{
    Transform storageArea; 
    Transform resourcesContainer; 
    private GameObject targetResource; 
    private NavMeshAgent agent;

    void Awake()
    {
        storageArea = GameManager.instance.storageArea;
        resourcesContainer = GameManager.instance.resourcesContainer;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        FindNewResource();
    }

    void Update()
    {
        if (targetResource != null)
        {
            if (Vector3.Distance(transform.position, targetResource.transform.position) < 1.0f)
            {
                agent.SetDestination(storageArea.position);
            }

            if (Vector3.Distance(transform.position, storageArea.position) < 1.0f)
            {
                Destroy(targetResource);
                FindNewResource();
            }
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
                agent.SetDestination(targetResource.transform.position);
            }
        }
    }
}
