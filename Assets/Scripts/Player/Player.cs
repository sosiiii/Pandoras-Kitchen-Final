using System;
using System.Collections;
using System.Collections.Generic;
using REWORK;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour, IKillable
{
    public static Action<GameObject, bool> playerDeath;
    //public Elevator elevator;

    //Horizontal
    [Header("Behaviour")]
    public int HP;
    private int maxHP;
    [SerializeField] PlayerHearts playerHearts;
    bool immune = false;
    [SerializeField] float immunityDuration = 1f;

    [SerializeField] private bool wasda;

    [Header("Move")]
    private float _horizontalMove;
    [SerializeField] private float horizontalSpeed;

    public bool playerIsFlipped;

    //Jump
    [Header("Jump")]
    [SerializeField] private float jumpPower;
    [SerializeField] private float gravityScale = 10;
    [SerializeField] private float fallingGravityScale = 40;
    
    public bool m_Grounded { get; private set; }
    [Header("Ground")]
    public float k_GroundedRadius = 0.2f;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private LayerMask m_WhatIsGround;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;

    private SpriteRenderer _spriteRenderer;

    public AudioClip[] playerJumpAudioClips;
    public AudioClip[] playerLandAudioClips;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        maxHP = HP;
    }

    private void Update()
    {
        _rigidbody2D.velocity = new Vector2(_horizontalMove * horizontalSpeed, _rigidbody2D.velocity.y);
        
        SetAnimation();
        JumpPhysics();
        FlipPlayer();
    }

    private void SetAnimation()
    {
        if (!m_Grounded)
        {
            _animator.SetFloat("speed-y", _rigidbody2D.velocity.y);
        }
        else
        {
            _animator.SetBool("Running", _rigidbody2D.velocity.x != 0);
            _animator.SetFloat("speed-y", 0);
        }
    }

    private void FixedUpdate()
    {
        var previousGrounded = m_Grounded;
        m_Grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                break;
            }
        }

        if (!previousGrounded && m_Grounded)
        {
            Debug.Log("I landed!");

            _animator.SetTrigger("Landed");

            AudioSource.PlayClipAtPoint(playerLandAudioClips[Random.Range(0, playerLandAudioClips.Length)], Camera.main.transform.position);
        }
        _animator.SetBool("OnGround", m_Grounded);

    }

    public void MovePlayer(InputAction.CallbackContext context)
    {
        _horizontalMove = context.ReadValue<float>();
    }

    private void FlipPlayer()
    {
        if(_horizontalMove > 0)
            transform.right = Vector3.right;
        else if(_horizontalMove < 0)
            transform.right = Vector3.right * -1;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if(!m_Grounded) return;

        AudioSource.PlayClipAtPoint(playerJumpAudioClips[Random.Range(0, playerJumpAudioClips.Length)], Camera.main.transform.position);

        _rigidbody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

    }
    private void JumpPhysics()
    {
        if (_rigidbody2D.velocity.y >= 0)
        {
            _rigidbody2D.gravityScale = gravityScale;
        }
        else if (_rigidbody2D.velocity.y < 0)
        {
            _rigidbody2D.gravityScale = fallingGravityScale;
        }
    }

    public void Kill()
    {
        DeathLogic();
    }

    private void DeathLogic()
    {
        _spriteRenderer.color = Color.white;
        _rigidbody2D.velocity = Vector2.zero;
        GetComponent<PlayerInteraction>().InventorySlot.RemoveItem();
        GetComponent<PlayerCombat>().attacking = false;
        HP = maxHP;
        playerDeath?.Invoke(gameObject, wasda);
        //Make player wait for some time
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(m_GroundCheck.position, k_GroundedRadius);
        
        
        
    }

    public void Damaged(int attackDamage)
    {
        if (immune) return;

        HP -= attackDamage;

        StartCoroutine(ColorHit());

        if (HP <= 0)
        {
            DeathLogic();
        }

        else
        {
            playerHearts.UpdateLifeVisual(HP);
            immune = true;

            Invoke("ResetImmunity", immunityDuration);
        }
    }

    public void ResetImmunity()
    {
        immune = false;
    }

    IEnumerator ColorHit()
    {
        var blinkWaitTime = new WaitForSeconds(0.2f);

        _spriteRenderer.color = Color.red;
        yield return blinkWaitTime;
        _spriteRenderer.color = Color.white;
    }
}
