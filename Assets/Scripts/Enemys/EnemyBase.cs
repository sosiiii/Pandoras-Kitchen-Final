using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public State state;

    public float speed;

    public Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeState(new Idle(this));
    }

    public void ChangeState(State newState)
    {
        if(state != null)
            state.Exit();
        state = newState;
        state.Enter();
    }

    private void Update()
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
}
