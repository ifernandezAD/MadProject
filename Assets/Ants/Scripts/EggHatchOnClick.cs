using System.Collections;
using UnityEngine;

public class EggHatchOnClick : MonoBehaviour
{
    [SerializeField] GameObject _full;
    [SerializeField] GameObject _damaged;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] GameObject antPrefab;

    void OnMouseDown()
    {
        DestroyEgg();
    }

    public void Clear()
    {
        _full.SetActive(true);
        _damaged.SetActive(false);
    }

    public void DestroyEgg()
    {
        _full.SetActive(false);
        _damaged.SetActive(true);
        _particleSystem.Play();

        StartCoroutine(DestroyDamagedAfterDelay());
    }

    IEnumerator DestroyDamagedAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        Destroy(_damaged);
        InstantiateAnt();
    }

    void InstantiateAnt()
    {
        if (antPrefab != null)
        {
            GameObject ant = Instantiate(antPrefab, transform.position, Quaternion.identity);
        }
    }
}