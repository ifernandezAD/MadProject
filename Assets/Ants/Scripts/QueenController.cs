using UnityEngine;

public class QueenController : MonoBehaviour
{
    [SerializeField] GameObject heartEffectPrefab;

    void OnMouseDown()
    {
        ShowHeartEffect();
        //GameManager.instance.AccelerateGeneration();
    }

    void ShowHeartEffect()
    {
        GameObject heartEffect = Instantiate(heartEffectPrefab, transform.position, Quaternion.identity);
        Destroy(heartEffect, 1.0f); 
    }
}