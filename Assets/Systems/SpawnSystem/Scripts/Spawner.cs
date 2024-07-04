using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{   
    [Header("References")]
    [SerializeField] Terrain terrain; 
    [SerializeField] GameObject resourcePrefab; 
    [SerializeField] GameObject spawnerContainer; 

    [Header("Variables")]
    [SerializeField] float maxSpawnHeight = 10f; 
    [SerializeField] float spawnYOffset = 0.5f;

    [Header("Restricted Areas")]
    [SerializeField] Transform storageAreaTransform; 
    [SerializeField] float storageAreaRadius;
    [SerializeField] Transform queenAreaTransform; 
    [SerializeField] float queenAreaRadius;

    [Header("Testing")]
    [SerializeField] bool testingSpawnResource;

    void OnValidate()
    {
        if(testingSpawnResource)
        {
            SpawnResource();
            testingSpawnResource=false;
        }
    }

    public void SpawnResource()
    {
        Vector3 spawnPosition = GetValidRandomPositionOnTerrain();
        spawnPosition.y += spawnYOffset;
        GameObject resource = Instantiate(resourcePrefab, spawnPosition, Quaternion.identity);
        resource.transform.parent = spawnerContainer.transform; 
    }

    Vector3 GetValidRandomPositionOnTerrain()
    {
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;

        while (true)
        {
            float randomX = Random.Range(0, terrainWidth);
            float randomZ = Random.Range(0, terrainLength);
            float terrainHeight = terrain.SampleHeight(new Vector3(randomX, 0, randomZ));
            Vector3 spawnPosition = new Vector3(randomX, terrainHeight, randomZ);

            if (terrainHeight <= maxSpawnHeight &&
                !IsInRestrictedArea(spawnPosition, storageAreaTransform.position, storageAreaRadius) &&
                !IsInRestrictedArea(spawnPosition, queenAreaTransform.position, queenAreaRadius))
            {
                return spawnPosition;
            }
        }
    }

    bool IsInRestrictedArea(Vector3 position, Vector3 areaCenter, float areaRadius)
    {
        return Vector3.Distance(position, areaCenter) <= areaRadius;
    }
}
