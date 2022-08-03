using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //public Elevator elevator;

    //Horizontal
    [Header("Move")]
    private float _horizontalMove;
    [SerializeField] private float horizontalSpeed;

    public bool playerIsFlipped;

    //Jump
    [Header("Jump")]
    [SerializeField] private float jumpPower;
    [SerializeField] private float gravityScale = 10;
    [SerializeField] private float fallingGravityScale = 40;

    /*[Header("Events")]
    [SerializeField] private UnityEvent OnLandEvent;*/

    [Header("Ground")]
    private bool m_Grounded;
    public float k_GroundedRadius = 0.2f;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private LayerMask m_WhatIsGround;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;

    /*private void Awake()
    {
        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }
    }*/

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _rigidbody2D.velocity = new Vector2(_horizontalMove * horizontalSpeed, _rigidbody2D.velocity.y);

        JumpPhysics();
        FlipPlayer();
    }

    private void FixedUpdate()
    {
        //bool wasGrounded = m_Grounded;
        m_Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                /*if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }*/
            }
        }
    }

    public void MovePlayer(InputAction.CallbackContext context)
    {
        _horizontalMove = context.ReadValue<float>();

        _animator.SetFloat("Speed", Mathf.Abs(_horizontalMove));
    }

    private void FlipPlayer()
    {
        if (_horizontalMove > 0f)
        {
            transform.eulerAngles = new Vector2(0, 0);
            playerIsFlipped = false;
        }

        else if (_horizontalMove < 0f)
        {
            transform.eulerAngles = new Vector2(0, 180);
            playerIsFlipped = true;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        if (m_Grounded)
        {
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

    /*public void OnLanding()
    {
        _animator.SetBool("IsJumping", false);
    }*/

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _animator.SetBool("IsJumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _animator.SetBool("IsJumping", true);
        }
    }
}
