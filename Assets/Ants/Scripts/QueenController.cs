using UnityEngine;

public class QueenController : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] GameObject eggPrefab; 
    [SerializeField] Transform spawnAreaCenter; 
    [SerializeField] float spawnAreaRadius = 150f; 

    [Header("Breeding")]
    [SerializeField] GameObject heartEffectPrefab;

    [Header("Testing")]
    public bool testingEggSpawn;

    void OnValidate()
    {
        if(testingEggSpawn)
        {
            SpawnEgg();
            testingEggSpawn = false;
        }
    }

    void OnMouseDown()
    {
        ShowHeartEffect();
        SpawnEgg();
    }

    void ShowHeartEffect()
    {
        GameObject heartEffect = Instantiate(heartEffectPrefab, transform.position, Quaternion.identity);
        Destroy(heartEffect, 1.0f); 
    }

    void SpawnEgg()
    {
        Vector3 spawnPosition = GetValidSpawnPosition();

        if (spawnPosition != Vector3.zero)
        {
            Instantiate(eggPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No valid spawn position found for egg.");
        }
    }

    Vector3 GetValidSpawnPosition()
    {
        int maxAttempts = 10;
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector2 randomPoint = Random.insideUnitCircle * spawnAreaRadius;
            Vector3 spawnPosition = new Vector3(spawnAreaCenter.position.x + randomPoint.x, spawnAreaCenter.position.y, spawnAreaCenter.position.z + randomPoint.y);

            if (!IsInQueenCollider(spawnPosition))
            {
                return spawnPosition;
            }
        }
        return Vector3.zero; 
    }

    bool IsInQueenCollider(Vector3 position)
    {
        Collider queenCollider = GetComponent<Collider>();
        return queenCollider.bounds.Contains(position);
    }
}
