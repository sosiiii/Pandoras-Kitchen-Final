using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidScript : MonoBehaviour
{
    public float HP;
    public float speed = 1;

    public Transform groundDetector;
    public Transform wallDetector;

    public Rigidbody2D rb;

    public bool right = true;
    public float direction = 1;
    [SerializeField] private LayerMask ground;



    [SerializeField] private Item deadEnemyItem;
    [SerializeField] private ItemObject itemObjectPrefab;

    private void Start()
    {
        Death();
    }

    private void Update()
    {
        
        //RaycastHit2D groundInfo = Physics2D.Raycast(groundDetector.position, Vector2.down, 2f, LayerMask.NameToLayer("Ground"));
        RaycastHit2D wallInfo = Physics2D.Raycast(transform.position, transform.right, 1.5f, ground);
        Debug.DrawRay(transform.position, transform.right, Color.red);
        if (wallInfo.collider != null)
        {
            transform.right *= -1;
            Debug.Log(("koliyia"));
        }
        rb.velocity = new Vector2(transform.right.x, rb.velocity.y) * speed;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.right * 10);
    }

    private void Death()
    {
        var itemObject = Instantiate(itemObjectPrefab, transform.position, Quaternion.identity);
                
        itemObject.Init(deadEnemyItem);
        Destroy(gameObject);
    }


}
