using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public enum SlimeState {
        Idle,
        Jump,
        InAir
    };

    private SlimeState state = SlimeState.Idle;

    [SerializeField] private float jumpValue;
    [SerializeField] private float jumpSideValueMin;
    [SerializeField] private float jumpSideValueMax;
    [SerializeField] PhysicsMaterial2D physicsMaterial2D;

    private bool grounded;
    [SerializeField] private float k_GroundedRadius = 0.2f;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private LayerMask m_WhatIsGround;

    Animator _animator;
    Rigidbody2D _rigidbody2D;
    CapsuleCollider2D _capsuleCollider2D;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        EnterIdle();
    }

    void Update()
    {
        switch (state)
        {
            case SlimeState.Idle:
                break;

            case SlimeState.InAir:
                if (grounded)
                {
                    state = SlimeState.Idle;
                    _capsuleCollider2D.sharedMaterial = physicsMaterial2D;
                    EnterIdle();
                }
                break;

        }
    }

    private void FixedUpdate()
    {
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true; 
            }
        }
    }

    private IEnumerator Wait()
    {
        _animator.SetBool("Jump", false);
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        state = SlimeState.Jump;
        EnterJump();
    }

    private void EnterIdle()
    {
        _capsuleCollider2D.sharedMaterial = null;
        StartCoroutine(Wait());
    }

    private void EnterJump()
    {
        var whatSide = Random.Range(0, 2);

        if (whatSide == 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }

        else if (whatSide == 1)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        whatSide = whatSide == 0 ? 1 : -1;

        _animator.SetBool("Jump", true);
        _rigidbody2D.AddForce(Vector2.up * jumpValue + Vector2.right * whatSide * Random.Range(jumpSideValueMin, jumpSideValueMax), ForceMode2D.Impulse);
        state = SlimeState.InAir;
    }

    /*private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _animator.SetBool("Jump", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _animator.SetBool("Jump", true);
        }
    }*/
}
