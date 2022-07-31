using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Serialization;
using woska_scripts;

[RequireComponent(typeof(BoxCollider2D), typeof(Highlight), typeof(Animator))]
public class Machine : MonoBehaviour, IInteractable
{
    private BoxCollider2D _boxCollider2D;
    private Animator _animator;
    public bool IsRunning { get; private set; }
    
    public bool FinishedItem { get; private set; }
    public bool FreeInputSlots => _inputSlots.Count < inputSlotsCount;
    public bool InputSlotsEmpty => _inputSlots.Count == 0;

    public bool OutPutSlotEmpty => outPutSlot == null;

    [SerializeField] private int inputSlotsCount = 2;
    private Stack<IPickable> _inputSlots = new Stack<IPickable>();

    public IPickable outPutSlot;


    [SerializeField] private Transform outPutSlotTransform;
    
    

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _boxCollider2D.isTrigger = true;
    }

    public bool Interact(PlayerInteract playerInteract)
    {
        if (IsRunning) return false;

        if (!OutPutSlotEmpty)
        {
            //Take item out
            if (playerInteract.HasItem) return false;
            
            
            playerInteract.PickUpItem(outPutSlot);
            outPutSlot = null;

        }
        else
        {
            if (playerInteract.HasItem && FreeInputSlots)
            {
                Debug.Log("Insert item");
                var itemInHand = playerInteract.currentItemInHand;

                playerInteract.ClearItemSlot();
                _inputSlots.Push(itemInHand);

                itemInHand.ChangeParent(transform);

                //Check for crafting
                
                if(_inputSlots.Count == 2)
                    StartCoroutine(CoroutineCrafting());
            }
            else if (!playerInteract.HasItem && !InputSlotsEmpty)
            {
                Debug.Log("Take item");
                var item = _inputSlots.Pop();
                playerInteract.PickUpItem(item);
            }
            else
            {
                Debug.Log("Player has nothing in his hands and machine is empty");
            }
        }




        return true;
    }

    private IEnumerator CoroutineCrafting()
    {
        IsRunning = true;
        _animator.Play("MachineWorking", 0, 0f);
        yield return new WaitForSeconds(4f);

        Destroy(_inputSlots.Pop().GetOwner());
        outPutSlot = _inputSlots.Pop();
        outPutSlot.ChangeParent(outPutSlotTransform);
        _animator.Play("MachineIdle", 0, 0f);
        IsRunning = false;
    }
}
