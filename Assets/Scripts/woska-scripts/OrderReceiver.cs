using System;
using UnityEngine;

namespace woska_scripts
{
    [RequireComponent(typeof(Highlight), typeof(BoxCollider2D))]
    public class OrderReceiver : MonoBehaviour, IInteractable
    {
        public static Action<Item> onOrderTurnedIn;
        public bool Interact(PlayerInteract playerInteract)
        {
            if (!playerInteract.ItemSlot.IsFull()) return false;
            
            Debug.Log("Was interacted");

            var item = playerInteract.ItemSlot.RemoveItem();
            
            onOrderTurnedIn?.Invoke(item);


            return true;
        }
    }
}