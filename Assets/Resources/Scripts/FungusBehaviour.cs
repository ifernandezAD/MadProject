using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusBehaviour : MonoBehaviour
{
    [SerializeField] GameObject smokeVfxPrefab;

    void OnMouseDown()
    {
        ShowSmokeffect();
    }

    void ShowSmokeffect()
    {
        GameObject smokeEffect = Instantiate(smokeVfxPrefab, transform.position, Quaternion.identity);
        Destroy(smokeEffect, 1.0f);
        Destroy(gameObject);
    }
}
