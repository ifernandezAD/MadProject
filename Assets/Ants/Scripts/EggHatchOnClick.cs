using System.Collections;
using UnityEngine;

public class EggHatchOnClick : MonoBehaviour
{
    [SerializeField] GameObject _full;
    [SerializeField] GameObject _damaged;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] GameObject antPrefab;

    private Transform workAntsContainer;

    private bool isDestroyed = false; 

    [SerializeField] AudioClip clickSound;
    private AudioSource audioSource;

    void Awake()
    {
        workAntsContainer = GameManager.instance.workAntsContainer;
        audioSource = GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        if (!isDestroyed)
        {
            PlayClickSound();
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
            GameObject ant = Instantiate(antPrefab, transform.position, Quaternion.identity);
            ant.transform.SetParent(workAntsContainer);
        }
    }

        void PlayClickSound()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);  
        }
    }
}
