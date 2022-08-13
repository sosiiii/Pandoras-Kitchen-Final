using System;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public bool OnGround { get; private set; }
    
    public float TimeLeftGround { get; private set; }

    private void OnCollisionEnter2D(Collision2D col)
    {
        EvaluateCollision(col);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EvaluateCollision(collision);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        OnGround = false;
        TimeLeftGround = Time.time;
    }

    private void EvaluateCollision(Collision2D collision2D)
    {
        TimeLeftGround = float.MinValue;
        foreach (var contactPoint in collision2D.contacts)
        {
            var normal = contactPoint.normal;
            OnGround |= normal.y >= 0.9f;
        }
    }
}
