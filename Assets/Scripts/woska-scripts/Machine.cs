using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Serialization;
using woska_scripts;
using Random = System.Random;

[RequireComponent(typeof(BoxCollider2D), typeof(Highlight), typeof(Animator))]
public class Machine : MonoBehaviour, IInteractable
{
    private BoxCollider2D _boxCollider2D;
    private Animator _animator;
    public bool IsRunning { get; private set; }
    
    public bool FinishedItem { get; private set; }
    public bool FreeInputSlots => _inputSlots.Count < inputSlotsCount;
    public bool InputSlotsEmpty => _inputSlots.Count == 0;
    

    [SerializeField] private int inputSlotsCount = 2;

    [SerializeField] private Transform _middlePoinTransform;
    [SerializeField] private Vector3 _offset = Vector3.right;

    
    [SerializeField] private List<IItemContainer> _inputSlots = new List<IItemContainer>();

    [SerializeField] private ItemSlot _outPutSlot;

    [SerializeField] private IItemContainer itemSlotPrefab;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _boxCollider2D.isTrigger = true;
        
        var tmp = (IItemContainer[])transform.GetChild(0).gameObject.GetComponentsInChildren<IItemContainer>();
   
        _inputSlots = new List<IItemContainer>(tmp);
        
        
        Debug.Log(_inputSlots.Count);
        
    }

    public bool Interact(PlayerInteract playerInteract)
    {
        var playerItemSlot = playerInteract.ItemSlot;

        if (playerItemSlot.IsFull())
        {
            if (FreeInputSlot())
            {
                InsertItem(playerInteract);
            }
        }
        else
        {
            if (_outPutSlot.IsFull())
            {
                //Take item out
            }
            else if (ContainsItem())
            {
                RemoveItem(playerInteract);
            }
        }

        return true;
    }

    bool FreeInputSlot()
    {
        foreach (var slot in _inputSlots)
        {
            if (!slot.IsFull()) return true;
        }

        return false;
    }
    bool ContainsItem()
    {
        foreach (var slot in _inputSlots)
        {
            if (slot.IsFull()) return true;
        }

        return false;
    }
    IItemContainer GetFirstItemSlot()
    {

        for (int i = _inputSlots.Count-1; i <= 0; i--)
        {
            var current = _inputSlots[i].IsFull();
            if (current) return _inputSlots[i];
        }

        return null;
    }

    IItemContainer GetFreeInputSlot()
    {
        foreach (var slot in _inputSlots)
        {
            if (!slot.IsFull()) return slot;
        }

        return null;
    }
    int FreeInputSlotsCount()
    {
        int free = 0;
        foreach (var slot in _inputSlots)
        {
            if (!slot.IsFull()) free++;
        }

        return free;
    }
    private void InsertItem(PlayerInteract playerInteract)
    {
        Debug.Log("Insert item");
        var itemInHand = playerInteract.ItemSlot.RemoveItem();

        var freeInputSlot = GetFreeInputSlot();
        
        Debug.Log(FreeInputSlotsCount());
        
        freeInputSlot.AddItem(itemInHand);
        
        UpdateSlotPosition();
    }

    private void UpdateSlotPosition()
    {
        var numberOfFreeSlots = FreeInputSlotsCount();

        if (numberOfFreeSlots == 2)
        {
            _inputSlots[0].SetLocalPosition(Vector3.zero);
            ;
        }
        else if (numberOfFreeSlots == 1)
        {
            _inputSlots[0].SetLocalPosition(-_offset * 0.5f);
            _inputSlots[1].SetLocalPosition(_offset * 0.5f);
        }
        else if (numberOfFreeSlots == 0)
        {
            _inputSlots[0].SetLocalPosition(-_offset);
            _inputSlots[1].SetLocalPosition(Vector3.zero);
            _inputSlots[2].SetLocalPosition(_offset);
        }
    }

    private void RemoveItem(PlayerInteract playerInteract)
    {
        var playerSlot = playerInteract.ItemSlot;

        var firstFullItemSlot = GetFirstItemSlot();

        var item = firstFullItemSlot.RemoveItem();


        playerSlot.AddItem(item);
        
        
        UpdateSlotPosition();


    }

    private IEnumerator CoroutineCrafting()
    {
        IsRunning = true;
        _animator.Play("MachineWorking", 0, 0f);
        yield return new WaitForSeconds(4f);
        
      
        _animator.Play("MachineIdle", 0, 0f);
        IsRunning = false;
    }
}
