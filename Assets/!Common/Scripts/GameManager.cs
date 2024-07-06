using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GamePhase
{
    Phase1,
    Phase2,
    Phase3,
    Phase4,
    Phase5
}

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [Header("References")]
    [SerializeField] public Transform storageArea;
    [SerializeField] public Transform resourcesContainer;
    [SerializeField] public Transform workAntsContainer;
    [SerializeField] public Transform zombiesAntContainer;
    [SerializeField] public Transform copAntContainer;
    [SerializeField] TextMeshProUGUI feedbackText;

    [Header("Resource Related")]
    [SerializeField] GameObject[] storageResourcePrefabs;
    [SerializeField] int resourceCount = 0;
    [SerializeField] int victoryResourceCount = 10;

    [Header("Game Settings")]
    [SerializeField] float phaseDuration = 60.0f;

    private GamePhase currentPhase = GamePhase.Phase1;

    [Header("Resource Spawning")]
    private float resourceSpawnInterval;
    private Coroutine resourceSpawnCoroutine;

    void Awake()
    {
        instance = this;
    }

    void OnEnable()
    {
        WorkerAnt.onResourceCollected += IncrementResourceCount;
        ZombieAnt.onNoWorkerAntsLeft+=GameOver;
    }


    void Start()
    {
        feedbackText.gameObject.SetActive(false);

        SetPhase(GamePhase.Phase1);
        StartCoroutine(PhaseTimer());
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
        feedbackText.gameObject.SetActive(true);
        feedbackText.text = "VICTORIA";
    }

    IEnumerator PhaseTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(phaseDuration);
            AdvancePhase();
        }
    }

    void AdvancePhase()
    {
        switch (currentPhase)
        {
            case GamePhase.Phase1:
                Debug.Log("Entering Phase 2");
                SetPhase(GamePhase.Phase2);
                break;
            case GamePhase.Phase2:
                Debug.Log("Entering Phase 3");
                SetPhase(GamePhase.Phase3);
                break;
            case GamePhase.Phase3:
                Debug.Log("Entering Phase 4");
                SetPhase(GamePhase.Phase4);
                break;
            case GamePhase.Phase4:
                Debug.Log("Entering Phase 5");
                SetPhase(GamePhase.Phase5);
                break;
            case GamePhase.Phase5:
                Debug.Log("Remaining in Phase 5");
                break;
            default:
                break;
        }
    }

    void SetPhase(GamePhase phase)
    {
        currentPhase = phase;

        if (resourceSpawnCoroutine != null)
        {
            StopCoroutine(resourceSpawnCoroutine);
        }

        switch (phase)
        {
            case GamePhase.Phase1:
                resourceSpawnInterval = 4f;
                break;
            case GamePhase.Phase2:
                resourceSpawnInterval = 3f;
                break;
            case GamePhase.Phase3:
                resourceSpawnInterval = 2f;
                break;
            case GamePhase.Phase4:
                resourceSpawnInterval = 1f;
                break;
            case GamePhase.Phase5:
                resourceSpawnInterval = 0.5f;
                break;
            default:
                break;
        }

        resourceSpawnCoroutine = StartCoroutine(SpawnResources());
    }

    IEnumerator SpawnResources()
    {
        while (true)
        {
            yield return new WaitForSeconds(resourceSpawnInterval);
            Spawner.instance.SpawnResource();
        }
    }

    public GamePhase GetCurrentPhase()
    {
        return currentPhase;
    }

    
    private void GameOver()
    {
        feedbackText.gameObject.SetActive(true);
        feedbackText.text = "DERROTA :(";
    }

    void OnDisable()
    {
        WorkerAnt.onResourceCollected -= IncrementResourceCount;
        ZombieAnt.onNoWorkerAntsLeft-=GameOver;
    }
}
