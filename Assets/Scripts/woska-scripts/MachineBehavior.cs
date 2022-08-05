using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Serialization;
using woska_scripts;
using Random = System.Random;

[RequireComponent(typeof(BoxCollider2D), typeof(Highlight), typeof(Animator))]
public class MachineBehavior : MonoBehaviour, IInteractable
{
    private BoxCollider2D _boxCollider2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    [SerializeField] private Machine _machine;
    private ItemSlot _outPutSlot;


    private ProgressBar _progressBarBehavior;


    private List<Item> _machineInventory = new List<Item>();

    private List<ItemSlot> _inputSlots;

    public bool CanInsertItem => _machineInventory.Count < _machine.MaxRecipeLength;

    public bool CraftingInProgress => _progressBarBehavior.IsRunning;

    public bool ItemIsCrafted => _outPutSlot.IsFull();

    private void OnValidate()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if(_machine == null) return;
        _spriteRenderer.sprite = _machine.Sprite;
    }

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        var parent = transform.GetChild(0);
        _progressBarBehavior = parent.GetChild(0).GetComponent<ProgressBar>();
        _outPutSlot = parent.GetChild(1).GetComponent<ItemSlot>();
        var slots = parent.GetChild(2).GetComponentsInChildren<ItemSlot>();
        
        Debug.Log(slots.Length);
       _inputSlots = new List<ItemSlot>(slots);
        
        
        _spriteRenderer.sprite = _machine.Sprite;
        _boxCollider2D.isTrigger = true;

    }
    private void OnEnable()
    {
        _progressBarBehavior.OnTimerRunout += CraftingDone;
    }
    private void OnDisable()
    {
        _progressBarBehavior.OnTimerRunout -= CraftingDone;
    }
    public bool Interact(PlayerInteract playerInteract)
    {
        var playerItemSlot = playerInteract.ItemSlot;
        
        if (ItemIsCrafted)
        {
            //Take item out
            if (!playerItemSlot.IsFull())
            {
                RemoveItem(playerInteract);
            }

            return true;
        }
        //Player has item in hand
        if (playerItemSlot.IsFull() && CanInsertItem)
        {
            InsertItem(playerInteract);
            if (CraftingInProgress)
            {
                _progressBarBehavior.UpdateBar(_machine.CraftingTime);
            }
            else
            {
                _progressBarBehavior.gameObject.SetActive(true);
                _progressBarBehavior.StartTimer(_machine.CraftingTime);
                _animator.Play("MachineWorking", 0, 0f);
            }
        }
        return true;
    }

    private void CheckRecipe()
    {
        //Check what can we craft from items from our inventory 


        _outPutSlot.AddItem(_machineInventory[_machineInventory.Count - 1]);
    }


    private void InsertItem(PlayerInteract playerInteract)
    {
        var itemInHand = playerInteract.ItemSlot.RemoveItem();
        
        _machineInventory.Add(itemInHand);
        
        foreach (var inputSlot in _inputSlots)
        {
            if(inputSlot.IsFull()) continue;
            inputSlot.AddItem(itemInHand);
            break;
        }
    }
    

    private void RemoveItem(PlayerInteract playerInteract)
    {
        var playerSlot = playerInteract.ItemSlot;
        var item = _outPutSlot.RemoveItem();

        playerSlot.AddItem(item);
    }
    
    private void CraftingDone()
    {
        
        _animator.Play("MachineIdle", 0, 0f);

        //Get result from crafting
        Item result = _machine.Trash;
        foreach (var craftingRecipe in _machine.CraftingRecipes)
        {
            if (craftingRecipe.CanCraft(_machineInventory, _machine))
            {
                result = craftingRecipe.Result;
                break;
            }
        }
        _machineInventory.Clear();

        
        //Deactivate all slots
        
        foreach (var inputSlot in _inputSlots)
        {
            inputSlot.RemoveItem();
        }

        
        //Put result to output slot
        
        _outPutSlot.AddItem(result);
        Debug.Log("Item crafted: " + result.itemName);

        
        //Deactivate timer
        Debug.Log("DONE");
    }
}
