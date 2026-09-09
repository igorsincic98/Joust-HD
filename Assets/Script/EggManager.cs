using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EggManager : MonoBehaviour
{
    [SerializeField] private float _eggHatchCooldown;
    [SerializeField] private GameObject _eggPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private ScoreManager _scoreManagerP1;
    [SerializeField] private ScoreManager _scoreManagerP2;
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private GameObject _eggTrigger;
    [SerializeField] private GameObject _eggTriggerPrefab;
    [SerializeField] private GameObject[] _entity;
    [SerializeField] public bool _grounded;
    [SerializeField] public bool _eggProcess;
    [SerializeField] public Rigidbody2D _rb;
    [SerializeField] public CircleCollider2D _circleCollider;

    private void Start()
    {
        _grounded = false;
        _spawnManager = GameObject.Find("Spawner Manager").GetComponent<SpawnManager>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _eggProcess = false;
    }

    void Update()
    {
        _eggHatchCooldown -= Time.deltaTime;
        if (_eggHatchCooldown <= 0)
        {
            Instantiate (_enemyPrefab, transform.position, transform.rotation);
            Destroy (gameObject);
        }
    }

    public void EggPickUp (GameObject other)
    {
        if (other.gameObject.name == "Player 1")
        {
            _scoreManagerP1 = GameObject.Find("Score P1").GetComponent<ScoreManager>();
            _scoreManagerP1.EggPickUp(other.gameObject);
            _spawnManager.EnemyDeath();
            Destroy(gameObject);
            return;
        }

        if (other.gameObject.name == "Player 2")
        {
            _scoreManagerP2 = GameObject.Find("Score P2").GetComponent<ScoreManager>();
            _scoreManagerP2.EggPickUp(other.gameObject);
            _spawnManager.EnemyDeath();
            Destroy(gameObject);
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_eggProcess == false && other.gameObject.name == "Player 1" || _eggProcess == false && other.gameObject.name == "Player 2")
        {
            _eggProcess = true;
            if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2")
            {
                EggPickUp(other.gameObject);
            }
        }
    }

    public void Touchdown()
    {
        Debug.Log("Egg has touchdown");
        _grounded = true;
        _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Kinematic;
        _eggTrigger = Instantiate(_eggTriggerPrefab, transform.position, transform.rotation);
        _eggTrigger.GetComponent<EggManager>()._eggHatchCooldown = Random.Range(9f, 12f);
        Destroy(gameObject);
    }
    
}
