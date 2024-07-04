using System.Collections;
using System.Collections.Generic;
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

    [Header("Resource Related")]
    [SerializeField] GameObject[] storageResourcePrefabs;
    [SerializeField] int resourceCount = 0;
    [SerializeField] int victoryResourceCount = 10;

    [Header("Game Settings")]
    [SerializeField] float phaseDuration = 60.0f; // Duración de cada fase en segundos

    private GamePhase currentPhase = GamePhase.Phase1;
    private float phaseTimer = 0.0f;

    void Awake()
    {
        instance = this;
        WorkerAnt.onResourceCollected += IncrementResourceCount;
    }

    void Start()
    {
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
        Debug.Log("Victory! You've collected enough resources.");
        // Aquí va la pantalla/mensaje de victoria. Por ejemplo, cartel de victoria y botón de replay.
    }

    IEnumerator PhaseTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(phaseDuration);

            // Avanzar a la siguiente fase
            switch (currentPhase)
            {
                case GamePhase.Phase1:
                    Debug.Log("Entering Phase 2");
                    // Lógica para la transición a la fase 2
                    break;
                case GamePhase.Phase2:
                    Debug.Log("Entering Phase 3");
                    // Lógica para la transición a la fase 3
                    break;
                case GamePhase.Phase3:
                    Debug.Log("Entering Phase 4");
                    // Lógica para la transición a la fase 4
                    break;
                case GamePhase.Phase4:
                    Debug.Log("Entering Phase 5");
                    // Lógica para la transición a la fase 5
                    break;
                case GamePhase.Phase5:
                    Debug.Log("Game Over - All phases completed.");
                    // Lógica para finalizar el juego, por ejemplo, pantalla de fin de juego.
                    break;
                default:
                    break;
            }

            currentPhase++;

            if (currentPhase > GamePhase.Phase5)
            {
                // Finalizar el juego si todas las fases han sido completadas
                WinGame();
                yield break;
            }
        }
    }
}
