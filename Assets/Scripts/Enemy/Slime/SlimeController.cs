using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour, IDamagable
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

    Animator _animator;
    Rigidbody2D _rigidbody2D;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
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
        }

        Death();
    }

    private void FixedUpdate()
    {
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
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

        if (whatSide == 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }

        else if (whatSide == 1)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        whatSide = whatSide == 0 ? 1 : -1;

        _rigidbody2D.AddForce(
            Vector2.up * jumpValue + Vector2.right * whatSide * Random.Range(jumpSideValueMin, jumpSideValueMax),
            ForceMode2D.Impulse);
        state = SlimeState.InAir;
    }

    public void Damaged(float attackDamage)
    {
        HP -= attackDamage;
    }

    public void Damage(float attackDamage, Vector3 knockbackDir)
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

            itemObject.Init(deadEnemyItem);
            Destroy(gameObject);
        }
    }
}
