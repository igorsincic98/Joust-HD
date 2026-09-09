using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] private SpriteRenderer _sr;
    private Vector2 _movement;
    [SerializeField] InputSystem_Actions _controls;
    
    [SerializeField] float _speed;
    [SerializeField] float _jumpingPower;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private Vector3 _scale;
    [SerializeField] private float _scaleX;
    [SerializeField] private float _reflect;

    private void Awake()
    {
        _maxSpeed = 50f;
        _controls = new InputSystem_Actions();
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _scale = transform.localScale;
        _scaleX = transform.localScale.x;
    }

    private void Update()
    {
        if (_rb.linearVelocity.x <= _maxSpeed)
        {
            _rb.AddForce(_movement * _speed, ForceMode2D.Force);
        }
        _reflect = -_rb.linearVelocityX;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            _scale.x = _scaleX;
            transform.localScale = _scale;
        }
        
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            _scale.x = -_scaleX;
            transform.localScale = _scale;
        }
        _movement = context.ReadValue<Vector2>();

        if (Gamepad.current != null)
        {
            if (Gamepad.current.leftStick.left.wasPressedThisFrame)
            {
                _scale.x = -_scaleX;
                transform.localScale = _scale;
            }

            if (Gamepad.current.leftStick.right.wasPressedThisFrame)
            {
                _scale.x = _scaleX;
                transform.localScale = _scale;
            }
        }

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _rb.AddForce(Vector2.up * _jumpingPower,  ForceMode2D.Impulse);
        }
        
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag != "PickUp")
        {
            _rb.linearVelocity = new Vector2 (_reflect * 0.75f, _rb.linearVelocityY);
        }
    }

}


