using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SquidScript : MonoBehaviour, IDamagable, IOnDeath
{

    public enum SquidStates{
        Patrol,
        Knockback,
        RunAway,
    }
    private SquidStates state;

    [Header("Behaviuor")]
    public float HP;
    public float speed = 1;

    [Header("RunAway")]
    [SerializeField] float SpeedUp;
    [SerializeField] float SpeedUpTime;

    [Header("Knockback")]
    public float knockbackForce;
    public float knockbackForceUp;

    [Header("Detectors")]
    public Transform groundDetector;
    public Transform wallDetector;

    [Header("Rigidbody")]
    public Rigidbody2D rb;

    [Header("Movement")]
    public bool right = true;
    public float direction = 1;
    [SerializeField] private LayerMask ground;

    [Header("Damage")]
    public GameObject DemageActivate;

    [Header("Items")]
    [SerializeField] private Item deadEnemyItem;
    [SerializeField] private ItemObject itemObjectPrefab;

    [Header("Ground")]
    private bool m_Grounded;
    public float k_GroundedRadius = 0.2f;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private LayerMask m_WhatIsGround;


    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        transform.right *= Random.value > 0.5 ? 1 : -1;
        state = SquidStates.Patrol;

    }

    

    private void Update()
    {
        switch (state)
        {
            case SquidStates.Patrol:
                Patrol();
                break;

            case SquidStates.Knockback:
                if (GroundCheck())
                {
                    state = SquidStates.Patrol;
                }
                break;
        }



        if (HP <= 0)
        {
            //Death();
        }
    }
    private bool GroundCheck()
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }
    private void Patrol()
    {

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetector.position, Vector2.down, 2f, ground);
        RaycastHit2D wallInfo = Physics2D.Raycast(transform.position, transform.right, 0.5f, ground);
        Debug.DrawRay(transform.position, transform.right, Color.red);
        if (wallInfo.collider != null || groundInfo.collider == false)
        {
            transform.right *= -1;
        }
        rb.velocity = new Vector2(transform.right.x, rb.velocity.y) * speed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.right * 0.5f);
    }

    private void Death()
    {
        var itemObject = Instantiate(itemObjectPrefab, transform.position, Quaternion.identity);
        
        DeathAction?.Invoke();
        itemObject.Init(deadEnemyItem);
        Destroy(gameObject);
    }
    public void Damaged(float attackDemage, Vector3 knockbackDir)
    {
        state = SquidStates.Knockback;
        HP -= attackDemage;
        if (HP <= 0)
        {
            Death();
            return;
        }

        StartCoroutine(ColorHit());
        StartCoroutine(RunAway());
        

        var dir = Vector2.zero;
        if (knockbackDir == Vector3.right)
        {
            dir = Vector2.right;
        }
        else
        {
            dir = Vector2.left;
        }
        
        rb.velocity = new Vector2(dir.x * knockbackForce, knockbackForceUp);
        transform.right = dir * Vector2.right;
        DemageActivate.SetActive(true);
    }

    private IEnumerator RunAway()
    {
        float startSpeed = speed;
        speed = SpeedUp;
        yield return new WaitForSeconds(SpeedUpTime);
        speed = startSpeed;
    }
    IEnumerator ColorHit()
    {
        var blinkWaitTime = new WaitForSeconds(0.1f);

        _spriteRenderer.color = Color.red;
        yield return blinkWaitTime;
        _spriteRenderer.color = Color.white;

    }
    public void Damage(float attackDemage, Vector3 knockbackDir)
    {
        Damaged(attackDemage, knockbackDir);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Action DeathAction { get; set; }
}
