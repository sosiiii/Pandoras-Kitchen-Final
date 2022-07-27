using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    public bool IsSlotFree { get; private set; } = true;
    private Item currentItem;
    [SerializeField] private Transform slotPosition;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !IsSlotFree)
        {
            DropItem();
        }
    }
    public void PickUpItem(Item item)
    {
        IsSlotFree = false;
        currentItem = item;
        currentItem.transform.parent = slotPosition;
        currentItem.transform.localPosition = Vector3.zero;
    }
    public void DropItem()
    {
        currentItem.IsPickedUp = false;
        currentItem.ChangeItemState();
        IsSlotFree = true;
        currentItem.transform.parent = null;
        currentItem = null;
    }


}
