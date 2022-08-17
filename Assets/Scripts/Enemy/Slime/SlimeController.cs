using System;
using System.Collections;
using System.Collections.Generic;
using REWORK;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlimeController : MonoBehaviour, IDamagable, IOnDeath, IKillable
{
    public enum SlimeState
    {
        Idle,
        Jump,
        InAir
    };

    private SlimeState state = SlimeState.Idle;

    [Header("Health")] public float HP;

    [Header("Jump")] [SerializeField] private float jumpValue;
    [SerializeField] private float jumpSideValueMin;
    [SerializeField] private float jumpSideValueMax;

    [Header("Items")] [SerializeField] private Item deadEnemyItem;
    [SerializeField] private ItemObject itemObjectPrefab;

    [Header("Ground")] public bool grounded;
    [SerializeField] private float groundedRadius = 0.2f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;

    public Transform attackPoint;
    public float attackRange;
    public int attackDamage;
    public LayerMask playerLayer;

    Animator _animator;
    Rigidbody2D _rigidbody2D;
    SpriteRenderer _spriteRenderer;
    public AudioManager audioManager;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
                    EnterIdle();
                }

                break;
        }

        if (grounded)
        {
            _animator.SetBool("Jump", false);
        }

        else if (!grounded)
        {
            _animator.SetBool("Jump", true);
            Hit();
        }
       
        Death();
    }

    private void FixedUpdate()
    {
        var prevGrounded = grounded;
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
            }
        }

        if (prevGrounded == false && grounded == true)
        {
            audioManager.PlaySound(1);
        }
    }

    public void Hit()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayers)
        {
            var playerHealth = player.GetComponent<Player>();

            if (playerHealth == null)
            {
                continue;
            }

            playerHealth.Damaged(attackDamage);
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

        if (whatSide == 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }

        else if (whatSide == 1)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        whatSide = whatSide == 0 ? 1 : -1;

        audioManager.PlaySound(0);
        _rigidbody2D.AddForce(
            Vector2.up * jumpValue + Vector2.right * whatSide * Random.Range(jumpSideValueMin, jumpSideValueMax),
            ForceMode2D.Impulse);
        state = SlimeState.InAir;
    }

    public void Damaged(int attackDamage)
    {
        HP -= attackDamage;

        StartCoroutine(ColorHit());        
    }

    IEnumerator ColorHit()
    {
        var blinkWaitTime = new WaitForSeconds(0.2f);

        _spriteRenderer.color = Color.red;
        yield return blinkWaitTime;
        _spriteRenderer.color = Color.white;

    }

    public void Damage(int attackDamage, Vector3 knockbackDir)
    {
        Damaged(attackDamage);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void Death()
    {
        if (HP <= 0)
        {
            var itemObject = Instantiate(itemObjectPrefab, transform.position, Quaternion.identity);

            DeathAction?.Invoke();
            itemObject.Init(deadEnemyItem);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public Action DeathAction { get; set; }
    public void Kill()
    {
        Destroy(gameObject);
        DeathAction?.Invoke();
        return;
        HP -= 1000;
        Death();
    }
}
