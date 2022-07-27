using UnityEngine;
using System.Collections;

public class InteractableOutline : MonoBehaviour
{
    [SerializeField] private Material OutLineMaterial;
    private SpriteRenderer SpriteRenderer;
    private Material DefaultMaterial;

    public bool IsDisplayed { get; private set; }

    private void Awake()
    {
        SetUp();
    }
    private void SetUp()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        DefaultMaterial = SpriteRenderer.sharedMaterial;
    }
    public void SetActive(bool active)
    {
        if (active)
        {
            IsDisplayed = true;
            SpriteRenderer.sharedMaterial = OutLineMaterial;
        }
        else
        {
            IsDisplayed = false;
            SpriteRenderer.sharedMaterial = DefaultMaterial;
        }
    }
}
