using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _speed;
    [SerializeField] private float _jumpingPower;
    [SerializeField] private float _directionCooldown;
    [SerializeField] private float _jumpCooldown;
    [SerializeField] private Vector2 _direction;
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private float _maxSpeed;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        var _dir = Random.Range(0, 2);
        if (_dir == 0)
        {
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
            _direction = new Vector2(1,0);
        }

        if (_dir == 1)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);

            _direction = new Vector2(-1,0);
        }
    }

    private void Update()
    {
        if (_directionCooldown <= 0f)
        {
            ChangeDirection();
        }
        _directionCooldown -= Time.deltaTime;
        if (_rb.linearVelocity.magnitude <= _maxSpeed)
        {
            _rb.AddForce(_direction * _speed, ForceMode2D.Force);
        }

        if (_jumpCooldown <= 0f)
        {
            _rb.AddForce(Vector2.up * _jumpingPower, ForceMode2D.Impulse);
            _jumpCooldown = Random.Range(0.1f, 0.5f);
        }
        _jumpCooldown -= Time.deltaTime;
        
    }

    public void ChangeDirection()
    {
        transform.localScale = new Vector2(-transform.localScale.x,transform.localScale.y);
        _direction.x = -_direction.x;
        _directionCooldown = Random.Range(7f, 10f);

    }
    
    
    
}
