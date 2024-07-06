using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEggOnTime : MonoBehaviour
{
    [Header("Egg Prefabs")]
    [SerializeField] GameObject blueEggPrefab;    

    [Header("Timer Settings")]
    [SerializeField] float changeTime = 10f;      

    void Start()
    {
        StartCoroutine(ChangeEggAfterTime());
    }

    IEnumerator ChangeEggAfterTime()
    {
        yield return new WaitForSeconds(changeTime);

        ChangeEgg();
    }

    void ChangeEgg()
    {
        
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
        
        Destroy(gameObject);

        Instantiate(blueEggPrefab, position, rotation);
    }
}
