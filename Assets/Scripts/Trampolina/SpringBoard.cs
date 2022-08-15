using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBoard : MonoBehaviour
{
    [SerializeField] private float strength = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer != LayerMask.NameToLayer("Players"))
            return;

        var player = col.GetComponent<Player>();
        
        if(player.m_Grounded) return;
        
        var rigidBody = player.GetComponent<Rigidbody2D>();
        var velocity = rigidBody.velocity;
        
        velocity.y = strength;

        rigidBody.velocity = velocity;
    }
}
