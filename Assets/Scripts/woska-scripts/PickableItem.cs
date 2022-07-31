using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(SpriteRenderer))]
[RequireComponent(typeof(Highlight))]
public class PickableItem : MonoBehaviour, IPickable
{
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer _spriteRenderer;

    public bool IsPickedUp { get; set; }

    [SerializeField] private SOItem _item;

    private void OnValidate()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if(_item == null) return;
        _spriteRenderer.sprite = _item.itemSprite;
    }

    public GameObject GetOwner()
    {
        return gameObject;
    }

    public GameObject PickUp()
    {
        if (IsPickedUp) return null;
        IsPickedUp = true;
        rigidbody2D.isKinematic = true;
        rigidbody2D.velocity = Vector2.zero;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        return gameObject;
    }

    public void Drop()
    {
        if(!IsPickedUp) return;
        IsPickedUp = false;
        rigidbody2D.isKinematic = false;
        transform.parent = null;
    }

    public void Throw()
    {
        if(!IsPickedUp) return;
        IsPickedUp = false;
        rigidbody2D.isKinematic = false;
        rigidbody2D.AddForce(transform.parent.right * 50f,ForceMode2D.Impulse);
        transform.parent = null;
        
    }

    public void ChangeParent(Transform parent)
    {
        transform.parent = parent;
        transform.localPosition = Vector3.zero;
    }
}
