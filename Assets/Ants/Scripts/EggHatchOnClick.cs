using System.Collections;
using UnityEngine;

public class EggHatchOnClick : MonoBehaviour
{
    [SerializeField] GameObject _full;
    [SerializeField] GameObject _damaged;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] GameObject antPrefab;

    private bool isDestroyed = false; 

    void OnMouseDown()
    {
        if (!isDestroyed)
        {
            DestroyEgg();
        }
    }

    public void Clear()
    {
        _full.SetActive(true);
        _damaged.SetActive(false);
        isDestroyed = false; 
    }

    public void DestroyEgg()
    {
        _full.SetActive(false);
        _damaged.SetActive(true);
        _particleSystem.Play();
        isDestroyed = true; 

        StartCoroutine(DestroyDamagedAfterDelay());
    }

    IEnumerator DestroyDamagedAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        Destroy(_damaged);
        InstantiateAnt();
        Destroy(gameObject);
    }

    void InstantiateAnt()
    {
        if (antPrefab != null)
        {
            Instantiate(antPrefab, transform.position, Quaternion.identity);
        }
    }
}
