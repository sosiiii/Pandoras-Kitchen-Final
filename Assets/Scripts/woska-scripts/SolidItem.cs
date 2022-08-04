using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(SpriteRenderer))]
[RequireComponent(typeof(Highlight))]
public class SolidItem : MonoBehaviour, IPickable
{
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer _spriteRenderer;

    public bool IsPickedUp { get; set; }

    [SerializeField] private Item _item;
    
    private const float THROW_STRENGTH = 50f;

    private void OnValidate()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_item == null) return;
        Init(_item);
        
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (_item == null) return;
        
        Init(_item);
    }

    public void Init(Item item)
    {
        _item = item;
        _spriteRenderer.sprite = _item.itemSprite;

       //var size = _spriteRenderer.sprite.bounds.size;
    }
    public Item GetItem()
    {
        return _item;
    }
    public void DestroyItem()
    { 
        Destroy(gameObject);
    }
    public void Throw()
    {
        _rigidbody2D.AddForce(transform.right * THROW_STRENGTH,ForceMode2D.Impulse);
    }
}
