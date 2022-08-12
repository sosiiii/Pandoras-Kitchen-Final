using System;
using System.Collections;
using System.Collections.Generic;
using REWORK;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 4f;

    private Vector2 _direction = Vector2.right;
    
    public void Init(Vector2 direction)
    {
        transform.right = direction;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.TryGetComponent(out IKillable killable))
            killable.Kill();
        Destroy(gameObject);
    }
}
