using System;
using System.Collections;
using System.Collections.Generic;
using REWORK;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 direction)
    {
        _rigidbody2D.velocity = direction * speed;

        transform.right = direction;
    }

    private void Start()
    {
        Destroy(gameObject, 10f);
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent(out Player player))
        {
            player.Damaged(1);
            Debug.Log("Take that");
        }
        Destroy(gameObject);
    }
}
