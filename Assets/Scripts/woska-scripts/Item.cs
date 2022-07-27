using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;

    public bool IsPickedUp { get; set; }


    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    public bool Interact(Interactor player)
    {
        var pickup = player.GetComponent<PickUp>();

        if (!pickup.IsSlotFree)
            return true;
        pickup.PickUpItem(this);
        IsPickedUp = true;
        ChangeItemState();
      
        return true;
    }

    public void ChangeItemState()
    {
        if(IsPickedUp)
        {
            rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            boxCollider2D.enabled = false;
        }
        else
        {
            rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            boxCollider2D.enabled = true;
        }
    }
}
