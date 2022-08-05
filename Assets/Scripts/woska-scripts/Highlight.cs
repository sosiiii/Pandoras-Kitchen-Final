using System;
using UnityEngine;
using System.Collections;

public class Highlight : MonoBehaviour
{
    [SerializeField] private Material OutLineMaterial;
    private SpriteRenderer SpriteRenderer;
    private Material DefaultMaterial;

    public bool IsToggled { get; private set; }

    private void Awake()
    {
        SetUp();
    }
    private void SetUp()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        DefaultMaterial = SpriteRenderer.sharedMaterial;
    }
    public void ToggleHighlight(bool toggle)
    {
        if (toggle && !IsToggled)
        {
            IsToggled = true;
            SpriteRenderer.sharedMaterial = OutLineMaterial;
        }
        else if (!toggle && IsToggled)
        {
            IsToggled = false;
            SpriteRenderer.sharedMaterial = DefaultMaterial;
        }
    }

    private void OnDrawGizmos()
    {
        
    }
}
