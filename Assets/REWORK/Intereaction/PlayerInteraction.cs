using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Intereaction;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{

    [SerializeField] private float interactionRadius = 1f;
    
    private Vector3 _interactionPoint => transform.position;
    private Interactable _current = null;

    private Collider2D[] _colliders;

    public InventorySlot InventorySlot { get; private set; }


    [SerializeField] private ItemObject itemObjectPrefab;

    private void Awake()
    {
        InventorySlot = GetComponentInChildren<InventorySlot>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_current != null) _current.ToggleHighlight(false);
       

        _colliders = Physics2D.OverlapCircleAll(_interactionPoint, interactionRadius, LayerMask.GetMask("Interactable"));
        if(_colliders.Length == 0) return;
        
        //Sorts base on distance and priority
        _colliders = _colliders.OrderBy((d) => (d.transform.position - _interactionPoint).sqrMagnitude).ToArray();
        _colliders = _colliders.OrderBy((d) => (d.GetPriority())).ToArray();
        
        
        _current = _colliders[0].GetInteractable();

        _current.ToggleHighlight(true);

    }
    public void HandleInput(InputAction.CallbackContext context)
    {
        if(!context.performed) return;

        if (_colliders.Length == 0)
        {
            if (!InventorySlot.IsFree)
            {
                if(Physics2D.OverlapCircle(InventorySlot.Position, 0.2f, LayerMask.GetMask("Ground"))) return;
                var item = InventorySlot.RemoveItem();

                var itemObject = Instantiate(itemObjectPrefab, InventorySlot.Position, Quaternion.identity);
                
                itemObject.Init(item);
            }
            return;
        }
        var current = _colliders[0].GetInteractable();

        switch (current.Type)
        {
            case Interactable.InteractionType.Click:
                current.Interact(this);
                break;
            case Interactable.InteractionType.Hold:
                current.Interact(this);
                break;
        }
    }

    public void HandleThrow(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        
        if (!InventorySlot.IsFree)
        {
            if(Physics2D.OverlapCircle(InventorySlot.Position, 0.2f, LayerMask.GetMask("Ground"))) return;

            var item = InventorySlot.RemoveItem();

            var itemObject = Instantiate(itemObjectPrefab, InventorySlot.Position, Quaternion.identity);
                
            itemObject.Init(item);
            itemObject.Throw(transform.right);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        
        Gizmos.DrawWireSphere(_interactionPoint, interactionRadius);
        
        if(_colliders == null) return;
        foreach (var collider in _colliders)
        {
            if(collider == null) break;
            var tmp = collider.GetComponent<Interactable>();
            Gizmos.color = tmp == _current ? Color.green : Color.red;
            
            var colliderPos = collider.transform.position;
            
            Gizmos.DrawLine(_interactionPoint, colliderPos);
        }
        
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(InventorySlot.Position, 0.2f);
    }
}
