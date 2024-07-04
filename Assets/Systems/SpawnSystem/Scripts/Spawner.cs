using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject resourcePrefab; // El prefab del recurso a spawnear
    public int resourceCount = 10; // Número de recursos a spawnear
    public Terrain terrain; // El terreno del hormiguero
    public float maxSpawnHeight = 10f; // Altura máxima para spawn en la zona baja
    public float minDistanceFromWalls = 5f; // Distancia mínima desde las paredes y obstáculos

    void Start()
    {
        SpawnResources();
    }

    void SpawnResources()
    {
        for (int i = 0; i < resourceCount; i++)
        {
            Vector3 spawnPosition = GetValidRandomPositionOnTerrain();
            Instantiate(resourcePrefab, spawnPosition, Quaternion.identity);
        }
    }

    Vector3 GetValidRandomPositionOnTerrain()
    {
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;

        while (true)
        {
            float randomX = Random.Range(minDistanceFromWalls, terrainWidth - minDistanceFromWalls);
            float randomZ = Random.Range(minDistanceFromWalls, terrainLength - minDistanceFromWalls);
            float terrainHeight = terrain.SampleHeight(new Vector3(randomX, 0, randomZ));
            
            if (terrainHeight <= maxSpawnHeight)
            {
                Vector3 spawnPosition = new Vector3(randomX, terrainHeight, randomZ);

                // Comprobar si el punto tiene suficiente espacio libre alrededor
                if (HasSufficientClearance(spawnPosition))
                {
                    return spawnPosition;
                }
            }
        }
    }

    bool HasSufficientClearance(Vector3 position)
    {
        // Realiza un sphere cast para verificar si hay colisiones cercanas
        float radius = minDistanceFromWalls;
        return !Physics.CheckSphere(position, radius);
    }

    void OnDrawGizmos()
    {
        if (terrain != null)
        {
            Gizmos.color = Color.green;
            float terrainWidth = terrain.terrainData.size.x;
            float terrainLength = terrain.terrainData.size.z;

            for (int i = 0; i < 100; i++) // Muestra 100 puntos de prueba
            {
                float randomX = Random.Range(minDistanceFromWalls, terrainWidth - minDistanceFromWalls);
                float randomZ = Random.Range(minDistanceFromWalls, terrainLength - minDistanceFromWalls);
                float terrainHeight = terrain.SampleHeight(new Vector3(randomX, 0, randomZ));

                if (terrainHeight <= maxSpawnHeight)
                {
                    Vector3 spawnPosition = new Vector3(randomX, terrainHeight, randomZ);
                    if (HasSufficientClearance(spawnPosition))
                    {
                        Gizmos.DrawSphere(spawnPosition, 1f);
                    }
                }
            }
        }
    }
}
