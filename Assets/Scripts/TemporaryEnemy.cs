using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryEnemy : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public float knockbackForce = 5;
    public float knockbackForceUp = 3;

    public Rigidbody2D rb;

    private void Start()
    {
        health = maxHealth;
    }

    public void Demaged(int demage, Transform player)
    {
        Debug.Log("Demaged");
        health -= demage;
        KnockBack(player);
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    void KnockBack(Transform player)
    {
        Vector2 knockbackDirection = new Vector2(transform.position.x - player.position.x, 0);
        rb.velocity = new Vector2(knockbackDirection.x, knockbackForceUp) * knockbackForce;
    }
}
