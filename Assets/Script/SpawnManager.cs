using System.Collections;
using System.Linq;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class SpawnManager : MonoBehaviour
{
    
    [SerializeField] private GameObject _enemy;

    [SerializeField] private GameObject[] _spawnPoints;

    [SerializeField] public int _enemyActive;
    
    [SerializeField] private float _respawnCooldown;

    [SerializeField] private GameObject _mainSpawner;
    
    [SerializeField] private GameObject _player1;
    
    [SerializeField] private GameObject _player2;
    
    [SerializeField] private LifeManager _lifeManager1;
    
    [SerializeField] private LifeManager _lifeManager2;

    [SerializeField] private GameObject _bounder;

    [SerializeField] private GameObject _hunter;
    
    [SerializeField] private GameObject _shadowLord;
    
    [SerializeField] private GameObject _egg;

    [SerializeField] private GameObject[] _platformList;
    
    [SerializeField] private GameObject _platformSpawn;
    
    [SerializeField] private GameObject _waveTitle;
    
    [SerializeField] private GameObject _survivalWaveTitle;

    [SerializeField] private TextMeshProUGUI _waveTitleText;
    
    [SerializeField] public string _waveInfo;
    
    [SerializeField] private float _platformMin;
 
    [SerializeField] private float _platformMax;
    
    [SerializeField] private Vector3 _playerSpawnPoint;
    
    [SerializeField] public int _enemyMax;

    [SerializeField] public int _bounderCount;
    
    [SerializeField] public int _hunterCount;
    
    [SerializeField] public int _shadowLordCount;
    
    [SerializeField] private int _enemyList;

    [SerializeField] public int _currentLife;
    
    [SerializeField] private WaitForSeconds _spawnCooldown;

    [SerializeField] private WaitForSeconds _waveTitleWait;

    [SerializeField] private WaitForSeconds _respawnWait;
    
    [SerializeField] private LevelManager _levelManager;
    
    [SerializeField] private PlayerManager _playerManager;
    
    [SerializeField] private ScoreManager _scoreManagerP1;
    
    [SerializeField] private ScoreManager _scoreManagerP2;
    
    [SerializeField] private PlayerInput _currentControlScheme;
    
    [SerializeField] private bool _waveStarted;
    
    [SerializeField] public bool _survivalWave;
    
    [SerializeField] private bool _hasPlayer1Died;
    
    [SerializeField] private bool _hasPlayer2Died;
    

    void Awake()
    {
        _mainSpawner = GameObject.Find("Main Spawner");
        _playerManager = GameObject.Find("Player Manager").GetComponent<PlayerManager>();
        _player1 = GameObject.Find("Player 1");
        _lifeManager1 = _player1.GetComponent<LifeManager>();
        _player2 = GameObject.Find("Player 2");
        _lifeManager2 = _player2.GetComponent<LifeManager>();
        _waveTitle = GameObject.Find("Wave Title");
        _survivalWaveTitle = GameObject.Find("Survival Wave Title");
        _survivalWaveTitle.SetActive(false);
        _waveTitleText = _waveTitle.GetComponent<TextMeshProUGUI>();
        _scoreManagerP1 = GameObject.Find("Score P1").GetComponent<ScoreManager>();
        _scoreManagerP2 = GameObject.Find("Score P2").GetComponent<ScoreManager>();
        _playerSpawnPoint = _mainSpawner.transform.position + new Vector3(0, 0.5f, 0);
        _enemyActive = 0;
        _spawnCooldown = new WaitForSeconds(1.5f);
        _waveTitleWait = new WaitForSeconds(4f); 
        _respawnWait = new WaitForSeconds(4f);
        _levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        _survivalWave = false;
        _hasPlayer1Died = false;
        _hasPlayer2Died = false;
        _currentLife = 2;
    }

    public void InitEnemySpawn()
    {
        StartCoroutine(EnemySpawn());
    }

    public void InitEggWave()
    {
        StartCoroutine(EggWave());
    }

    public IEnumerator EnemySpawn()
    {
        _platformList = GameObject.FindGameObjectsWithTag("Platform");
        _waveTitleText.text = _waveInfo;
        _waveTitle.SetActive(true);
        if (_survivalWave == true)
        {
            _survivalWaveTitle.SetActive(true);
        }

        yield return _waveTitleWait ;
        _waveTitle.SetActive(false);
        _survivalWaveTitle.SetActive(false);
        Debug.Log("Initializing");
            Debug.Log("Ready to Spawn");

        for (int i = 0; i < _bounderCount; i++)
        {
            Debug.Log("Spawning" + i);
            Instantiate(_bounder, (_spawnPoints[Random.Range(0, _spawnPoints.Length)].transform.position), Quaternion.identity);
            _enemyActive++; 
            yield return _spawnCooldown;
        }

        for (int i = 0; i < _hunterCount; i++)
        {
            Instantiate(_hunter, _spawnPoints[Random.Range(0, _spawnPoints.Length)].transform.position, Quaternion.identity);
            _enemyActive++;
            yield return _spawnCooldown;
        }

        for (int i = 0; i < _shadowLordCount; i++)
        {
            Instantiate(_shadowLord, _spawnPoints[Random.Range(0, _spawnPoints.Length)].transform.position, Quaternion.identity);
            _enemyActive++;
            yield return _spawnCooldown;
        }
        _waveStarted = true;
    }

    public IEnumerator EggWave()
    {
        _waveTitleText.text = _waveInfo;
        _waveTitle.SetActive(true);
        yield return _waveTitleWait ;
        _waveTitle.SetActive(false);
        for (int i = 0; i < 9; i++)
        {
            _platformSpawn = _platformList[Random.Range(0, _platformList.Length)];
            _platformMin = _platformSpawn.transform.GetChild(0).transform.position.x;
            _platformMax = _platformSpawn.transform.GetChild(1).transform.position.x;
            Instantiate(_egg,new Vector3(Random.Range(
                _platformMin, _platformMax), _platformSpawn.transform.position.y, 0), Quaternion.identity);
        }
        _waveStarted = true;
    }

    public void EnemyDeath()
    {
        _enemyActive--;
        if (_enemyActive == 0 && _waveStarted == true)
        {
            if (_survivalWave && _player1.activeSelf && _hasPlayer1Died == false)
            {
                _scoreManagerP1.SurvivalWaveBonusInit(_player1);
            }

            if (_survivalWave && _player2.activeSelf && _hasPlayer2Died == false)
            {
                _scoreManagerP2.SurvivalWaveBonusInit(_player2);
            }
        _levelManager.NextLevel();
        _waveStarted = false;
        _hasPlayer1Died = false;
        _hasPlayer2Died = false;
        _survivalWave = false;
        }

    }

    public void PlayerRespawnInit(GameObject player)
    {
       StartCoroutine(PlayerRespawn(player));
    }
    public IEnumerator PlayerRespawn(GameObject player)
    {
        if (player.tag == "Player1")
        {
            _hasPlayer1Died = true;
        }

        if (player.tag == "Player2")
        {
            _hasPlayer2Died = true;
        }
        Debug.Log(player.tag);
        _currentControlScheme = player.GetComponent<PlayerInput>();
        player.SetActive(false);
        if (player.GetComponent<LifeManager>()._gameOver == false)
        {
            Debug.Log(player + "respawn in process");
            yield return new WaitForSeconds(3f);
            player.transform.position = _playerSpawnPoint;
            player.SetActive(true);
            if (player.tag == "Player1")
            {
                if (_playerManager._P1Scheme == "Keyboard&Mouse")
                {
                    player.GetComponent<PlayerInput>().SwitchCurrentControlScheme(Keyboard.current);
                }

                if (_playerManager._P1Scheme == "Gamepad")
                {
                    player.GetComponent<PlayerInput>().SwitchCurrentControlScheme(Gamepad.current);
                }
            }
            if (player.tag == "Player2")
            {
                if (_playerManager._P2Scheme == "Keyboard&Mouse")
                {
                    player.GetComponent<PlayerInput>().SwitchCurrentControlScheme(Keyboard.current);
                }

                if (_playerManager._P2Scheme == "Gamepad")
                {
                    player.GetComponent<PlayerInput>().SwitchCurrentControlScheme(Gamepad.current);
                }
            }
        }
    }
}
