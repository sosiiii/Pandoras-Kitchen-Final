
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ItemObject : Interactable
{
    [SerializeField] private Item _itemData;
    [SerializeField] private float throwForce = 50f;

    private Rigidbody2D _rigidbody2D;

    private void OnValidate()
    {

    }

    protected override void Awake()
    {
        base.Awake();
        if (_itemData == null) return;

        SpriteRenderer.sprite = _itemData.Sprite;

        _rigidbody2D = GetComponent<Rigidbody2D>();

    }

    public void Init(Item item)
    {
        _itemData = item;
        SpriteRenderer.sprite = _itemData.Sprite;
    }

    public void Throw(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction*throwForce, ForceMode2D.Impulse);
    }

    public override void Interact(PlayerInteraction playerInteraction)
    {
        if(playerInteraction.InventorySlot.IsFree)
            HandlePickUp(playerInteraction);
    }

    private void HandlePickUp(PlayerInteraction playerInteraction)
    {
        playerInteraction.InventorySlot.AddItem(_itemData);
        Destroy(gameObject);
    }
}
