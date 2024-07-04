using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenController : MonoBehaviour
{
    [SerializeField] GameObject heartEffectPrefab;
    [SerializeField] float accelerationFactor = 0.9f;
    [SerializeField] float acceleratedGenerationInterval = 5.0f;
    [SerializeField] float accelerationDuration = 5.0f;

    private float initialGenerationInterval = 20.0f;
    private float currentGenerationInterval;
    private bool isAccelerated = false;

    void Start()
    {
        currentGenerationInterval = initialGenerationInterval;
        StartCoroutine(GenerateWorkerAnts());
    }

    void OnMouseDown()
    {
        ShowHeartEffect();

        if (!isAccelerated)
        {
            StartCoroutine(AccelerateGeneration());
        }
    }

    IEnumerator GenerateWorkerAnts()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentGenerationInterval);
            // Lógica para generar hormigas aquí
            currentGenerationInterval *= accelerationFactor; // Reducir el intervalo de generación
        }
    }

    IEnumerator AccelerateGeneration()
    {
        isAccelerated = true;
        float previousGenerationInterval = currentGenerationInterval;
        currentGenerationInterval = Mathf.Min(currentGenerationInterval, acceleratedGenerationInterval);
        yield return new WaitForSeconds(accelerationDuration);
        currentGenerationInterval = previousGenerationInterval;
        isAccelerated = false;
    }

    void ShowHeartEffect()
    {
        GameObject heartEffect = Instantiate(heartEffectPrefab, transform.position, Quaternion.identity);
        Destroy(heartEffect, 1.0f); // Ajusta el tiempo según la duración del efecto
    }
}
