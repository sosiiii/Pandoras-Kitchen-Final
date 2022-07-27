using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public State state;

    public float knockbackForce = 5;
    public float knockbackForceUp = 3;

    public float speed;

    public Rigidbody2D rb;

    public int health;
    public int maxHealth;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeState(new Idle(this));

        health = maxHealth;
    }  

    public void ChangeState(State newState)
    {
        if(state != null)
            state.Exit();
        state = newState;
        state.Enter();
    }

    private void FixedUpdate()
    {
        state.Process();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ChangeState(new Move(this, collision.GetComponent<Player>()));
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ChangeState(new Idle(this));
        }
    }

    public void Demaged(int damage, Transform player)
    {
        ChangeState(new Demaged(this, damage, player, knockbackForce, knockbackForceUp));
    }
}
