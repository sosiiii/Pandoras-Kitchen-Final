using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidBase : MonoBehaviour
{
    public static int enemiesCount = 0;
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
    public Transform RayShooter;

    [Header("JumpTimer")]
    public float Timer;
    public float minTime;
    public float maxTime;

    public bool MoveState;
    public GameObject StunedEnemy;


    [Header("Patroling")]       
    public float patrolingSpeed;
    public float patrolingSpeedMax;
    public float patrolingSpeedMin;
    public float patrolTimer;

    public Vector2 patrol1;
    public Vector2 patrol2;

    public LayerMask PatrolLayerMask;

    public float patrolWait;


    public bool patroling;

    public bool canTimerRun = true;

    public RaycastHit2D hit;

    private void Start()
    {
        enemiesCount++;
        rb = GetComponent<Rigidbody2D>();
        ChangeState(new Idle(this));
        StartCoroutine(DelayedStart());

        health = maxHealth;

    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(5);
        if (!MoveState)
        {
            ChangeState(new Patroling(this));
        }
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

    /*public void TriggerEnter(Player player)
    {
        StopAllCoroutines();
        ChangeState(new Move(this, player));
    }

    public void TriggerExit()
    {

        ChangeState(new Idle(this));

        MoveState = false;
        StartCoroutine(Delay());
    }*/
    public void Move(Transform player)
    {
        ChangeState((new Move(this, player)));
    }
    IEnumerator Delay()
    {
        
        yield return new WaitForSeconds(5);
        Debug.LogError("patroling changed");
        ChangeState(new Patroling(this));
    }

    public void Demaged(int damage, Transform player)
    {
        ChangeState(new Demaged(this, damage, player, knockbackForce, knockbackForceUp));
    }

    public bool Grounded()
    {
        if (!Physics2D.Raycast(transform.position, -transform.up, 50, PatrolLayerMask))
        {
            hit = Physics2D.Raycast(transform.position, -transform.up, 50, PatrolLayerMask);
            return true;
        }
        return false;
    }

    public void Stunned()
    {
        ChangeState(new Stuned(this));
    }
    /*void OnCollisionEnter2D(Collision2D collision)
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
    }*/
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

        if (patroling && canTimerRun)
        {
            patrolTimer -= Time.deltaTime;
        }
    }

    private void OnDestroy()
    {
        enemiesCount--;
    }
}