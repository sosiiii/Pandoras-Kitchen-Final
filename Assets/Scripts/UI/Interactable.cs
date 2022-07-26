using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(CircleCollider2D), typeof(SpriteRenderer))]
public abstract class Interactable : MonoBehaviour
{

    [field: SerializeField] protected Material OutLineMaterial { get; private set; } = null;
    protected SpriteRenderer SpriteRenderer;
    protected Material DefaultMaterial;


    public bool CanBeInteractedWith { get; private set; }
    public bool IsActivated { get; private set; } = false;

    public abstract void Interaction();

    private void OnValidate()
    {
        SetUp();
    }
    private void SetUp()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();

        DefaultMaterial = SpriteRenderer.sharedMaterial;
    }

    public void Activate()
    {
        if (IsActivated) return;

        IsActivated = true;

        SpriteRenderer.sharedMaterial = OutLineMaterial;
    }
    public void DeActivate()
    {

        if (!IsActivated) return;
        IsActivated = false;
        SpriteRenderer.sharedMaterial = DefaultMaterial;
    }
}
