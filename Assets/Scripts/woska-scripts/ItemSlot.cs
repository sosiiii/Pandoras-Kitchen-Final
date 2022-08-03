using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using woska_scripts;

public class ItemSlot : MonoBehaviour, IItemContainer
{
    [field: SerializeField] public Image ItemIcon { get; private set; }
    [field: SerializeField] public Image SlotIcon { get; private set; }

    private Item _item = null;

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
        ItemIcon.sprite = null;
        ToggleSlot(false);
        return tmpItem;
    }
    public bool AddItem(Item item)
    {
        if (IsFull()) return false;
        _item = item;
        ItemIcon.sprite = item.itemSprite;
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
        SlotIcon.gameObject.SetActive(toggle);
    }
}
