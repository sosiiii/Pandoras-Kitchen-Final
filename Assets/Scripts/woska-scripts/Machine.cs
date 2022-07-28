using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using woska_scripts;

[RequireComponent(typeof(BoxCollider2D), typeof(Highlight), typeof(Animator))]
public class Machine : MonoBehaviour, IInteractable
{
    private BoxCollider2D _boxCollider2D;
    private Animator _animator;
    public bool IsRunning { get; private set; }
    
    public bool FinishedItem { get; private set; }
    public bool HasFreeSlots { get; private set; }

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _boxCollider2D.isTrigger = true;
    }

    public bool Interact(PlayerInteract playerInteract)
    {
        if (IsRunning) return false;

        StartCoroutine(CoroutineCrafting());

        if (FinishedItem)
        {
            // We must take item out
            
            //If we have empty hand we take item out
            
            
            //If we have full hand ... nothing
        }
        else if(HasFreeSlots)
        {
            if (playerInteract.HasItem)
            {
                //Insert item
            }
        }
        
        

        if (playerInteract.currentItemInHand != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator CoroutineCrafting()
    {
        IsRunning = true;
        _animator.Play("MachineWorking", 0, 0f);
        yield return new WaitForSeconds(2f);
        _animator.Play("MachineIdle", 0, 0f);
        IsRunning = false;
    }
}
