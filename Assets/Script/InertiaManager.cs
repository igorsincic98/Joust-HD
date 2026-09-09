using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;

public class InertiaManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Vector2 _direction;
    [SerializeField] private Transform _transformParent;
    [SerializeField] private GameObject _parent;
    [SerializeField] private Vector2 _inertia;
    [SerializeField] private Vector2 _reflect;
    [SerializeField] private float _speed;
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private PlayerMovement _playerMovement;
    void Awake()
    {
        _parent = gameObject.transform.parent.GameObject();
        _transformParent = _parent.transform;
        _rb = _parent.GetComponent<Rigidbody2D>();
        _enemyMovement = _parent.GetComponent<EnemyMovement>();
        _playerMovement = _parent.GetComponent<PlayerMovement>();
        _inertia = _rb.linearVelocity;
        _speed = _rb.linearVelocity.magnitude;


    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        /*
        if (_parent.tag == "Player1" || _parent.tag == "Player2")
        {
            Debug.Log(_inertia);
            _reflect = Vector2.Reflect(_rb.linearVelocity.normalized, collision.contacts[0].normal);
            _rb.linearVelocity = _reflect * _speed;
            return;
        }
        */

        if (other.tag == "Bounder" || other.tag == "Hunter" || other.tag == "ShadowLord" || other.tag == "Player1"  || other.tag == "Player2" || other.tag == "EnemyJoust" || other.tag == "Tilemap")
        {
            _enemyMovement.ChangeDirection();
            _rb.linearVelocity = new Vector2(-_rb.linearVelocity.x * 0.75f, _rb.linearVelocity.y);
            return;
        }
    }
}
