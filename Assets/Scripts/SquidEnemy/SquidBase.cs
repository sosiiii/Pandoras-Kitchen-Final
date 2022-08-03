using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidBase : MonoBehaviour
{
    public SquidState state;

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

    public GameObject StunedEnemy;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeState(new Idle(this));
        StartCoroutine(DelayedStart());

        health = maxHealth;
        grounded = true;

    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(5);
        ChangeState(new Patroling(this));
    }

    public void ChangeState(SquidState newState)
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

    IEnumerator IdleToPatrol()
    {
        yield return new WaitForSeconds(3);
        ChangeState(new Patroling(this));
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(5);
        if (!MoveState)
        {
            Debug.LogError("patroling changed");
            ChangeState(new Patroling(this));
        }
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

        if (health <= 0)
        {
            ChangeState(new Stuned(this));
        }
    }
}