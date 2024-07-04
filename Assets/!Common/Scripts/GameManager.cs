using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [Header("References")]
    [SerializeField] public Transform storageArea; 
    [SerializeField] public Transform resourcesContainer; 

    void Awake()
    {
        instance = this;
    }
}
