using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance { get; private set; }
    private List<GameObject> resources = new List<GameObject>();

    void Awake()
    {
        instance = this;
    }

    public void AddResource(GameObject resource)
    {
        resources.Add(resource);
    }

    public GameObject GetRandomResource()
    {
        if (resources.Count > 0)
        {
            int randomIndex = Random.Range(0, resources.Count);
            GameObject selectedResource = resources[randomIndex];
            resources.RemoveAt(randomIndex); 
            return selectedResource;
        }
        else
        {
            return null; 
        }
    }
}
