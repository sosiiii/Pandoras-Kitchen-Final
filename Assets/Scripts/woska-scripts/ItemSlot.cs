using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using woska_scripts;

[RequireComponent(typeof(SpriteRenderer))]
public class ItemSlot : MonoBehaviour, IItemContainer
{
    private SpriteRenderer _itemIcon;

    private Item _item = null;
    private void Awake()
    {
        _itemIcon = GetComponent<SpriteRenderer>();
        //ToggleSlot(false);
    }

    private void Start()
    {
        ToggleSlot(false);
    }

    public bool ContainsItem(Item item)
    {
        return _item == item && item != null;
    }
    public Item RemoveItem()
    {
        if (!IsFull()) return null;

        var tmpItem = _item;
        _item = null;
        _itemIcon.sprite = null;
        ToggleSlot(false);
        return tmpItem;
    }
    public bool AddItem(Item item)
    {
        if (IsFull()) return false;
        _item = item;
        _itemIcon.sprite = item.itemSprite;
        ToggleSlot(true);
        return true;
    }
    public bool IsFull()
    {
        return _item != null;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }

    private void ToggleSlot(bool toggle)
    {
        _itemIcon.gameObject.SetActive(toggle);
    }
}
