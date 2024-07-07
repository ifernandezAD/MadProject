using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusBehaviour : MonoBehaviour
{
    [SerializeField] GameObject smokeVfxPrefab;
    [SerializeField] AudioClip clickSound;
    private AudioSource audioSource;
    private Renderer fungusRenderer;
    private Collider fungusCollider;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
         fungusRenderer = GetComponent<Renderer>();
        fungusCollider = GetComponent<Collider>();
    }

    void OnMouseDown()
    {
        PlayClickSound();
        ShowSmokeffect();
    }
    void ShowSmokeffect()
    {
        GameObject smokeEffect = Instantiate(smokeVfxPrefab, transform.position, Quaternion.identity);
        Destroy(smokeEffect, 1.0f);
        HideVisuals();
        Destroy(gameObject,1.0f);
    }

    void PlayClickSound()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

        void HideVisuals()
    {
        fungusRenderer.enabled = false;
        fungusCollider.enabled = false;
    }
}
