
using System.Collections;
using System.Collections.Generic;
using Intereaction;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class MachineObject : Interactable
{
    public enum MachineStates
    {
        Idle,
        Working,
        Done
    }
    
    [field: SerializeField] public Machine Machine { get; private set; }
    public MachineStates State { get; private set; } = MachineStates.Idle;
    
    private Animator _animator;

    private int _workProgress = 0;

    private List<Item> _machineInventory = new List<Item>();

    public bool FreeInputSlot => _machineInventory.Count < Machine.MaxRecipeLength;

    [SerializeField] private InventorySlot _outPutSlot;

    [SerializeField] private InventorySlot[] _inputSlots;


    [SerializeField] private ProgressBar _progressBar;


    private float maxTime = 0;
    private float currentTime = 0f;

    [SerializeField] private Sprite _slicerSprite;
    
    
    
    protected override void Awake()
    {
        base.Awake();

        _animator = GetComponent<Animator>();

        SpriteRenderer.sprite = Machine.Sprite;
        Debug.Log(SpriteRenderer.sprite);
    }

    public override void Interact(PlayerInteraction playerInteraction)
    {
        var slot = playerInteraction.InventorySlot;
        switch (State)
        {
            case MachineStates.Idle:
                //Brake if player has empty hands OR wants to insert invalid item
                if(slot.IsFree) return;
                if(!FreeInputSlot) return;
                
                if(Machine.CantProccessEnemies && slot.ItemData.IsEnemy) return;
                
                InsertToSlot(slot.RemoveItem());
                
                State = MachineStates.Working;
                EnterWorking();
                break;
            case MachineStates.Working:
                //If player has empty hands and Type is Needs input ->> handle
                if (slot.IsFree && Machine.Type == Machine.MachineType.NeedsInput)
                {
                    // Handle input
                    _workProgress--;
                    StartCoroutine(ChangeSprite());
                }
                //If player has full hands and item is valid and has free slots ---> handle
                else if (!slot.IsFree)
                {
                    if(!FreeInputSlot) return;
                    if(Machine.CantProccessEnemies && slot.ItemData.IsEnemy) return;

                    InsertToSlot(slot.RemoveItem());
                    //Increase time
                    maxTime += Machine.CraftingTime;
                    currentTime += (float)Machine.CraftingTime;
                }

                State = MachineStates.Working;
                break;
            case MachineStates.Done:
                if(!slot.IsFree) return;
                
                slot.AddItem(_outPutSlot.RemoveItem());

                State = MachineStates.Idle;
                break;
        }
    }

    private void EnterWorking()
    {
        if (Machine.Type == Machine.MachineType.Automatic)
        {
            _animator.Play("MachineWorking");
            StartCoroutine(MachineWorking());
            _progressBar.Init(Machine.CraftingTime);
        }
        else
        {
            StartCoroutine(MachineMakeWork());
            _progressBar.Init(Machine.CraftingTime);
        }

    }
    private IEnumerator MachineWorking()
    { 
        currentTime = (float)Machine.CraftingTime;
        maxTime = Machine.CraftingTime;
        var timeDecrese = 1f;
        var wait = new WaitForSeconds(timeDecrese);
        _progressBar.UpdateBar(currentTime,maxTime);
        while (currentTime > 0)
        {
            yield return wait;
            currentTime -= timeDecrese;
            
            _progressBar.UpdateBar(currentTime,maxTime);
        }
        _progressBar.StopTimer();
        ExitWorking();
    }

    private IEnumerator ChangeSprite()
    {
        SpriteRenderer.sprite = _slicerSprite;
        yield return new WaitForSeconds(0.2f);
        SpriteRenderer.sprite = Machine.Sprite;

    }
    private IEnumerator MachineMakeWork()
    {
        _workProgress = Machine.CraftingTime;
        while (_workProgress > 0)
        {
            yield return null;
            _progressBar.UpdateBar(_workProgress,Machine.CraftingTime);
        }
        _progressBar.StopTimer();
        ExitWorking();
    }

    private void ExitWorking()
    {
        //Stop to play animation
    

        EnterDone();
        State = MachineStates.Done;
    }

    private void InsertToSlot(Item item)
    {
        for (int i = 0; i < Machine.MaxRecipeLength; i++)
        {
            var slot = _inputSlots[i];
            if(!slot.IsFree) continue;
            
            slot.AddItem(item);
            _machineInventory.Add(item);
            break;
                
        }
    }

    private void EnterDone()
    {
        _workProgress = 0;
        
        _animator.Play("MachineIdle");
        
        //Craft item
        var item = Machine.Craft(_machineInventory);
        
        _machineInventory.Clear();
        foreach (var slot in _inputSlots) slot.RemoveItem();
        
        //Add crafted item to outputslot
        _outPutSlot.AddItem(item);
    }
}
