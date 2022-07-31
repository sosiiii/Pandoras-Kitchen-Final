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

    public int jumpChanceNumber = 1000000;
    public float jumpForce;
    public bool grounded;

    [Header("JumpTimer")]
    public float Timer;
    public float minTime;
    public float maxTime;

    public bool MoveState;

    public LayerMask PatrolLayerMask;

    public float patrolWait;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeState(new Patroling(this));

        health = maxHealth;
        grounded = true;

    }

    public void ChangeState(State newState)
    {
        if (state != null)
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

            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(5);
        if(!MoveState)
            ChangeState(new Patroling(this));
    }

    public void Demaged(int damage, Transform player)
    {
        ChangeState(new Demaged(this, damage, player, knockbackForce, knockbackForceUp));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }
    /*
    private void Update()
    {
        state.Updating();
    }*/

    private void Update()
    {
        
        if (MoveState == true)
        {
            Timer -= Time.deltaTime;
        }
        else if(MoveState == false)
        {
            Timer = 0;
        }
    }
}