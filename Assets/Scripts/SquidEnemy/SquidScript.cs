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
    private void Update()
    {
        
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetector.position, Vector2.down, 2f, LayerMask.NameToLayer("Ground"));
        RaycastHit2D wallInfo = Physics2D.Raycast(transform.position, Vector2.right, 10f, LayerMask.NameToLayer("Ground"));
        rb.velocity = new Vector2(transform.right.x, rb.velocity.y) * speed;
        
        if (/*groundInfo.collider == false || */wallInfo.collider != null)
        {
            transform.right *= -1;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.right * 10);
    }


}
