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
    [SerializeField] PhysicsMaterial2D physicsMaterial2D;

    private bool grounded;
    [SerializeField] private float k_GroundedRadius = 0.2f;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private LayerMask m_WhatIsGround;

    Rigidbody2D _rigidbody2D;
    CapsuleCollider2D _capsuleCollider2D;

    void Awake()
    {
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
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        state = SlimeState.Jump;
        EnterJump();
    }

    private void EnterIdle()
    {
        StartCoroutine(Wait());
    }

    private void EnterJump()
    {
        var whatSide = Random.Range(0, 2);

        whatSide = whatSide == 0 ? 1 : -1;

        _rigidbody2D.AddForce(Vector2.up * jumpValue + Vector2.right * whatSide * Random.Range(2, 10), ForceMode2D.Impulse);
        state = SlimeState.InAir;
    }
}
