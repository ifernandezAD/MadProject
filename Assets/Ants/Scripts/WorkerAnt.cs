using System;
using UnityEngine;
using UnityEngine.AI;

public class WorkerAnt : MonoBehaviour
{
    public static Action onResourceCollected;

    [SerializeField] private GameObject targetResource;
    private Transform storageArea;
    private Transform resourcesContainer;
    private NavMeshAgent agent;
    private Animator animator;

    [Header("Zombie Ant")]
    [SerializeField] private GameObject zombieAntPrefab; 
     private Transform zombieAntContainer; 

    void Awake()
    {
        storageArea = GameManager.instance.storageArea;
        resourcesContainer = GameManager.instance.resourcesContainer;
        zombieAntContainer = GameManager.instance.zombiesAntContainer;

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
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
        animator.SetBool("isWalking", agent.velocity.magnitude > 0.1f);
    }

    void FindNewResource()
    {
        if (resourcesContainer != null && resourcesContainer.childCount > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, resourcesContainer.childCount);
            targetResource = resourcesContainer.GetChild(randomIndex).gameObject;
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Resource"))
        {
            if (targetResource != null && other.gameObject == targetResource)
            {
                CollectResource();
            }
        }

        if (other.gameObject.CompareTag("Fungus"))
        {
            if (targetResource != null && other.gameObject == targetResource)
            {
                ConvertToZombieAnt();
            }
        }
    }

    void CollectResource()
    {
        onResourceCollected?.Invoke();
        Destroy(targetResource);
        targetResource = null;
    }

    void ConvertToZombieAnt()
    {
        Destroy(targetResource);
        Destroy(gameObject);

        GameObject zombieAnt = Instantiate(zombieAntPrefab, transform.position, Quaternion.identity);
        
        if (zombieAntContainer != null)
        {
            zombieAnt.transform.parent = zombieAntContainer;
        }
    }
}
