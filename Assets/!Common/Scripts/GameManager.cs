using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [Header("References")]
    [SerializeField] public Transform storageArea; 
    [SerializeField] public Transform resourcesContainer; 

    [Header("Resource Related")]
    [SerializeField] GameObject[] storageResourcePrefabs;
    [SerializeField] int resourceCount = 0;
    [SerializeField] int victoryResourceCount = 10;

    void Awake()
    {
        instance = this;
        WorkerAnt.onResourceCollected += IncrementResourceCount;
    }

    void IncrementResourceCount()
    {
        resourceCount++;

        int randomIndex = Random.Range(0, storageResourcePrefabs.Length);
        GameObject resourcePrefab = storageResourcePrefabs[randomIndex];

        Instantiate(resourcePrefab, storageArea);

        if (resourceCount >= victoryResourceCount)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        Debug.Log("Victory! You've collected enough resources.");
        // Aquí va la pantalla/mensaje de victoria. Se me ocurre que las hormigas bailen o salten, cartel de victoria y boton de replay. 
    }
}
