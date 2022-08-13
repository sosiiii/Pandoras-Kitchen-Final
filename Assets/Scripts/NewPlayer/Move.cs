using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Ground), typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{

    [Header("Move")] 
    [SerializeField] private float maxSpeed = 4f;
    [SerializeField] private float groundAcceleration = 35f;
    [SerializeField] private float airAcceleration = 20f;


    private Vector2 _direction;
    private Vector2 _velocity;
    private Vector2 _desiredVelocity;
    


    private bool _onGround;
    private float _acceleration;
    private float _maxSpeedChange;
    
    
    private Ground _ground;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _ground = GetComponent<Ground>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _desiredVelocity = Vector2.right * (_direction.x * Mathf.Max(maxSpeed, 0));
    }

    private void FixedUpdate()
    {
        _onGround = _ground.OnGround;
        _velocity = _rigidbody2D.velocity;

        _acceleration = _onGround ? groundAcceleration : airAcceleration;

        _maxSpeedChange = _acceleration * Time.deltaTime;

        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);


        _rigidbody2D.velocity = _velocity;
    }

    public void ReadMoveInput(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<float>() * Vector2.right;
    }
}
