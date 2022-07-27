using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUp : MonoBehaviour
{

    public bool IsSlotFree { get; private set; } = true;
    private Item currentItem;
    [SerializeField] private Transform slotPosition;


    private void Update()
    {
        return;
        if (Keyboard.current.eKey.wasPressedThisFrame && !IsSlotFree)
        {
            DropItem();
        }
    }
    public void PickUpItem(Item item)
    {
        if(item.IsPickedUp) return;


        
        IsSlotFree = false;
        currentItem = item;
        currentItem.IsPickedUp = true;
        currentItem.transform.parent = slotPosition;
        currentItem.transform.localPosition = Vector3.zero;
        
        currentItem.ChangeItemState();
    }
    public void DropItem()
    {
        if(!currentItem.IsPickedUp) return;
        currentItem.IsPickedUp = false;
        currentItem.ChangeItemState();
        IsSlotFree = true;
        currentItem.transform.parent = null;
        currentItem = null;
    }


}
