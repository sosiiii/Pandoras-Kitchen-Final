using System;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public abstract class Interactable : MonoBehaviour
{
    public enum InteractionType
    {
        Click,
        Hold
    }
    
    protected SpriteRenderer SpriteRenderer;

    [SerializeField] private Material outLineMaterial;
    private Material _defaultMaterial;

    private bool _highlighted;
    
    [field: SerializeField] public InteractionType Type { get; private set; }
    [field: SerializeField] public int Priority { get; private set; }

    protected virtual void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    protected void Start()
    {
        _defaultMaterial = SpriteRenderer.sharedMaterial;
    }
    
    public virtual void ToggleHighlight(bool toggle)
    {
        if(_highlighted == toggle) return;

        SpriteRenderer.sharedMaterial = toggle ? outLineMaterial : _defaultMaterial;

        _highlighted = !_highlighted;

    }
    
    public abstract void Interact(PlayerInteraction playerInteraction);

}
