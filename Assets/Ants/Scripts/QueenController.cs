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
        if (testingEggSpawn)
        {
            TrySpawnEgg();
            testingEggSpawn = false;
        }
    }

    void OnMouseDown()
    {
        ShowHeartEffect();
        TrySpawnEgg();
    }

    void ShowHeartEffect()
    {
        GameObject heartEffect = Instantiate(heartEffectPrefab, transform.position, Quaternion.identity);
        Destroy(heartEffect, 1.0f);
    }

    void TrySpawnEgg()
    {
        float spawnProbability = GetSpawnProbability();
        if (Random.value <= spawnProbability)
        {
            SpawnEgg();
        }
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

    float GetSpawnProbability()
    {
        GamePhase currentPhase = GameManager.instance.GetCurrentPhase();
        switch (currentPhase)
        {
            case GamePhase.Phase1:
                return 0.05f;  
            case GamePhase.Phase2:
                return 0.1f; 
            case GamePhase.Phase3:
                return 0.2f; 
            case GamePhase.Phase4:
                return 0.4f; 
            case GamePhase.Phase5:
                return 0.5f; 
            default:
                return 0.0f;
        }
    }
}
