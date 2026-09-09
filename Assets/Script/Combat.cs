using System;
using System.Security;
using UnityEngine;
using Random = UnityEngine.Random;

public class Combat : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    [SerializeField] private Rigidbody2D _playerRigidBody;

    [SerializeField] private SpawnManager SpawnManager;

    [SerializeField] private GameObject _eggPrefab;

    [SerializeField] private GameObject _egg;

    [SerializeField] private bool _invulCombat;

    [SerializeField] private float _invulCooldown;

    [SerializeField] private ScoreManager _scoreManagerP1;

    [SerializeField] private ScoreManager _scoreManagerP2;

    [SerializeField] private GameObject _scoreTextP1;
    
    [SerializeField] private GameObject _scoreTextP2;

    [SerializeField] private LifeManager _lifeManager;

    [SerializeField] private float _respawnCooldown;

    [SerializeField] private float _respawnInvul;

    [SerializeField] private GameObject _mainSpawner;

    [SerializeField] private Vector3 _playerSpawnPoint;

    [SerializeField] private string _targetType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        SpawnManager = GameObject.Find("Spawner Manager").GetComponent<SpawnManager>();
        _invulCombat = false;
        _player = gameObject.transform.parent.gameObject;
        _playerRigidBody = _player.GetComponent<Rigidbody2D>();
        _scoreTextP1 = GameObject.Find("Score P1");
        _scoreManagerP1 = _scoreTextP1.GetComponent<ScoreManager>();
        _scoreTextP2 = GameObject.Find("Score P2");
        _scoreManagerP2 = _scoreTextP2.GetComponent<ScoreManager>();
        _lifeManager = _player.GetComponent<LifeManager>();
        _playerSpawnPoint = _mainSpawner.transform.position + new Vector3(0, 0.5f, 0);
        _player.transform.position = _playerSpawnPoint;
        _respawnInvul = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_invulCooldown > 0)
        {
            _invulCooldown -= Time.deltaTime;
        }

        if (_invulCooldown <= 0)
        {
            _invulCombat = false;
        }

        if (_respawnInvul > 0)
        {
            _respawnInvul -= Time.deltaTime;
        }

    }

    public void Init()
    {
        if (gameObject.tag == "Player1")
        {
            _scoreTextP1.SetActive(true);
            return;
        }

        if (gameObject.tag == "Player2")
        {
            _scoreTextP2.SetActive(true);
            return;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Contact");
        Debug.Log(other.gameObject.name);
        if (!_invulCombat && _respawnInvul <= 0)
        {
            Debug.Log("Combat Check");


            if (other.gameObject.tag == "EnemyJoust")
            {
                Debug.Log("Hit");
                _invulCombat = true;
                _invulCooldown = 0.01f;
                if (other.gameObject.transform.position.y < _player.transform.position.y)
                {
                    Debug.Log("Win");
                    _targetType = other.gameObject.transform.parent.gameObject.tag;
                    _egg = Instantiate(_eggPrefab, other.transform.position, Quaternion.identity);
                    _egg.GetComponent<Rigidbody2D>()
                        .AddForce(new Vector2(Random.Range(0f, 50f), Random.Range(0f, 75f)), ForceMode2D.Impulse);
                    Destroy(other.gameObject.transform.parent.gameObject);
                    if (gameObject.tag == "Player1")
                    {
                        Debug.Log("P1 Score");
                        _scoreManagerP1.EnemyScore(_targetType,_player);
                        return;
                    }

                    if (gameObject.tag == "Player2")
                    {
                        Debug.Log("P2 Score");
                        _scoreManagerP2.EnemyScore(_targetType, _player);
                        return;
                    }
                }

                if (other.gameObject.transform.position.y > _player.transform.position.y)
                {
                    Debug.Log("Lose");
                    _lifeManager.LifeDown();
                    SpawnManager.PlayerRespawnInit(_player);
                    _invulCooldown = 1f;
                    _invulCombat = true;
                    return;

                }
            }

            if (other.gameObject.tag == "Player1" && other.gameObject.name == "Attack" || other.gameObject.tag == "Player2" && other.gameObject.name == "Attack")
                {
                    Debug.Log("Hit");
                    _invulCombat = true;
                    _invulCooldown = 0.01f;
                    if (other.gameObject.transform.position.y < _player.transform.position.y)
                    {
                        Debug.Log("Win");
                        _targetType = other.gameObject.transform.parent.tag;
                        Debug.Log(other.gameObject.transform.parent.tag);
                        other.gameObject.GetComponent<LifeManager>().LifeDown();
                        Destroy(other.gameObject.transform.parent.gameObject);
                        if (gameObject.tag == "Player1")
                        {
                            Debug.Log("P1 Score");
                            _scoreManagerP1.EnemyScore(_targetType, _player);
                            return;
                        }

                        if (gameObject.tag == "Player2")
                        {
                            Debug.Log("P2 Score");
                            _scoreManagerP2.EnemyScore(_targetType,_player);
                            return;
                        }
                    }

                    if (other.gameObject.transform.position.y > _player.transform.position.y)
                    {
                        Debug.Log("Lose");
                        _lifeManager.LifeDown();
                        SpawnManager.PlayerRespawnInit(_player);
                        _invulCooldown = 1f;
                        _invulCombat = true;
                        return;

                    }

                }
            }
        }
    }
