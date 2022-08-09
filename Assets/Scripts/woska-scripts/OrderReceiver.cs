using System;
using UnityEngine;

public class OrderReceiver : Interactable
{
    public static Action<Item> orderTurnedIn;
    public override void Interact(PlayerInteraction playerInteraction)
    {
        var slot = playerInteraction.InventorySlot;
        
        if(slot.IsFree) return;

        var item = playerInteraction.InventorySlot.RemoveItem();
        orderTurnedIn?.Invoke(item);
    }
}
