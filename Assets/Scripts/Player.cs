using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public GameObject elevator;

    //Horizontal
    [Header("Move")]
    private float _horizontalMove;
    [SerializeField] private float horizontalSpeed;

    //Jump
    [Header("Jump")]
    [SerializeField] private float jumpPower;
    [SerializeField] private float gravityScale = 10;
    [SerializeField] private float fallingGravityScale = 40;

    [Header("Events")]
    [SerializeField] private UnityEvent OnLandEvent;

    [Header("Ground")]
    private bool m_Grounded;
    const float k_GroundedRadius = 1f;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private LayerMask m_WhatIsGround;

    private SpriteRenderer _spriteRend;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Jump();
        JumpPhysics();
        MovePlayer();
        FlipPlayer();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
            }
        }
    }

    private void MovePlayer()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal");

        _animator.SetFloat("Speed", Mathf.Abs(_horizontalMove));

        _rigidbody2D.velocity = new Vector2(_horizontalMove * horizontalSpeed , _rigidbody2D.velocity.y);
    }

    private void FlipPlayer()
    {
        if (_horizontalMove > 0f)
        {
            transform.eulerAngles = new Vector2(0, 0); 
        }

        else if (_horizontalMove < 0f)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    private void Jump()
    {
        if (m_Grounded && Input.GetButtonDown("Jump"))
        {
            _animator.SetBool("IsJumping", true);
            _rigidbody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
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

    public void OnLanding()
    {
        _animator.SetBool("IsJumping", false);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Elevator")
        {
            gameObject.transform.parent = elevator.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Elevator")
        {
            gameObject.transform.parent = null;
        }
    }
}
