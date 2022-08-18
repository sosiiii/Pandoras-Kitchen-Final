using System;
using DG.Tweening;
using UnityEngine;

public class OrderReceiver : Interactable
{
    public static Action<Item> orderTurnedIn;

    [SerializeField] private GameObject _topChest;
    public override void Interact(PlayerInteraction playerInteraction)
    {
        var slot = playerInteraction.InventorySlot;
        
        if(slot.IsFree) return;

        var item = playerInteraction.InventorySlot.RemoveItem();
        orderTurnedIn?.Invoke(item);
    }

    public override void ToggleHighlight(bool toggle)
    {
        base.ToggleHighlight(toggle);
        
        if(toggle)
            _topChest.transform.DOLocalRotate(new Vector3(0, 0, -90), 1f);
        else
            _topChest.transform.DOLocalRotate(new Vector3(0, 0, 0), 1f);
        
    }
    
}
