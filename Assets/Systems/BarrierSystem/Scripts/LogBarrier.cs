using UnityEngine;
using UnityEngine.AI;

public class LogBarrier : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] Material visibleMaterial;
    [SerializeField] Material transparentMaterial;
    [SerializeField] Renderer logRenderer;

    [Header("Components")]
    [SerializeField] NavMeshObstacle navMeshObstacle;

    private bool isActive = false;

    void Start()
    {
        SetTransparent();
        navMeshObstacle.enabled = false;
    }

    void OnMouseDown()
    {
        if (isActive)
        {
            SetTransparent();
            navMeshObstacle.enabled = false;
        }
        else
        {
            SetVisible();
            navMeshObstacle.enabled = true;
        }

        isActive = !isActive;
    }

    void SetTransparent()
    {
        logRenderer.material = transparentMaterial;
    }

    void SetVisible()
    {
        logRenderer.material = visibleMaterial;
    }
}
