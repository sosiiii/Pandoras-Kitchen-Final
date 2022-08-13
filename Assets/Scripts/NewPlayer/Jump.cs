using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class Jump : MonoBehaviour
{
    [Header("Jump")] 
    [SerializeField, Range(1f, 3f)] private float jumpHeight = 1f;
    [SerializeField, Range(0f, 5f)] private float downMovementMultiplayer = 1f;
    [SerializeField, Range(0f, 5f)] private float upMovementMultiplayer = 1f;

    [SerializeField, Range(0f, 1f)] private float coyoteTime = 0.1f;
    [SerializeField, Range(0f, 1f)] private float jumpBuffer = 0.1f;

    private Vector2 _velocity;
    
    private float _defaultGravityScale;

    private bool _jumpInputDown;
    private bool _onGround;
    
    private Ground _ground;
    private Rigidbody2D _rigidbody2D;

    private float _timeLeftGround;
    private float _timeLastJumpPressed;
    
    private bool HasBufferedJump => _onGround && _timeLastJumpPressed + jumpBuffer > Time.time;
    private bool HasCoyoteTime => !_onGround && _timeLeftGround + coyoteTime > Time.time;

    private void Awake()
    {
        _ground = GetComponent<Ground>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _defaultGravityScale = _rigidbody2D.gravityScale;
    }

    
    private void FixedUpdate()
    {
        _velocity = _rigidbody2D.velocity;
        _onGround = _ground.OnGround;
        _timeLeftGround = _ground.TimeLeftGround;


        if (_jumpInputDown && HasCoyoteTime || HasBufferedJump)
        {
            JumpAction();
        }
        _rigidbody2D.velocity = _velocity;

        if (_velocity.y > 0)
        {
            _rigidbody2D.gravityScale = upMovementMultiplayer;
        }
        else if (_velocity.y < 0)
        {
            _rigidbody2D.gravityScale = downMovementMultiplayer;
        }
        else
        {
            _rigidbody2D.gravityScale = _defaultGravityScale;
        }
    }

    private void JumpAction()
    {
        float jumpSpeed = Mathf.Sqrt(-2f * (Physics2D.gravity.y * upMovementMultiplayer) * jumpHeight);
        if (_velocity.y > 0f)
        {
            jumpSpeed = Mathf.Max(jumpSpeed - _velocity.y, 0f);
            
        }

        _velocity.y += jumpSpeed;
        
    }
    public void ReadJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _timeLastJumpPressed = Time.time;
            _jumpInputDown = true;
        }
        else if(context.performed)
        {
            _jumpInputDown = true;
        }
        else
        {
            _jumpInputDown = false;
        }

    }
}
