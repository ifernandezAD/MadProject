using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
[Header("Tutorial Images")]
    [SerializeField] private Transform imageContainer; 
    [SerializeField] private TextMeshProUGUI subtitleText; 
    [SerializeField] private string[] subtitles; 

    private int currentIndex = 0;

    void Start()
    {
        if (imageContainer.childCount != subtitles.Length)
        {
            Debug.LogError("Number of images and subtitles do not match!");
            return;
        }

        // Initialize the tutorial
        ShowCurrentImageAndSubtitle();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AdvanceTutorial();
        }
    }

    void ShowCurrentImageAndSubtitle()
    {
 
        for (int i = 0; i < imageContainer.childCount; i++)
        {
            imageContainer.GetChild(i).gameObject.SetActive(false);
        }

        
        imageContainer.GetChild(currentIndex).gameObject.SetActive(true);

        
        subtitleText.text = subtitles[currentIndex];
    }

    void AdvanceTutorial()
    {
        
        currentIndex++;

        
        if (currentIndex >= imageContainer.childCount)
        {
            EndTutorial();
        }
        else
        {
            ShowCurrentImageAndSubtitle();
        }
    }

    void EndTutorial()
    {
        
        for (int i = 0; i < imageContainer.childCount; i++)
        {
            imageContainer.GetChild(i).gameObject.SetActive(false);
        }

        subtitleText.gameObject.SetActive(false);

        
        Debug.Log("Tutorial finished!");
    }
}
