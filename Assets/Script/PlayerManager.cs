using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public GameObject _player1;
    [SerializeField] public GameObject _player2;
    [SerializeField] private GameObject _inputP1;
    [SerializeField] private GameObject _inputP2;
    [SerializeField] public bool _hasP1Joined = false;
    [SerializeField] public bool _hasP2Joined = false;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private bool _keyboardInput = false;
    [SerializeField] private bool _gamepadInput = false;
    [SerializeField] private GameObject _scoreP1;
    [SerializeField] private GameObject _scoreP2;
    [SerializeField] private TextMeshProUGUI _waveTitleText;
    [SerializeField] private GameObject _waveTitle;
    [SerializeField] private WaitForSeconds _gameOverWait;
    [SerializeField] public string _P2Scheme;
    [SerializeField] public string _P1Scheme;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _player1 = GameObject.Find("Player 1");
        _player2 = GameObject.Find("Player 2");
        _inputP1 = GameObject.Find("Input P1");
        _inputP2 = GameObject.Find("Input P2");
        _scoreP1 = GameObject.Find("Score P1");
        _scoreP2 = GameObject.Find("Score P2");
        _waveTitle =  GameObject.Find("Wave Title");
        _waveTitleText = _waveTitle.GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        _player1.SetActive(false);
        Debug.Log(_player1.activeInHierarchy);
        _player2.SetActive(false);
        Debug.Log(_player2.activeInHierarchy);
        _scoreP1.SetActive(false);
        _scoreP2.SetActive(false);
        _levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        _gameOverWait = new WaitForSeconds(3f);

    }

    private void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame && _keyboardInput == false)
        {
            PlayerHasJoined("Keyboard&Mouse");
        }

        if (Gamepad.current != null)
        {
            if (Gamepad.current.startButton.wasPressedThisFrame && _gamepadInput == false)
            {
                PlayerHasJoined("Gamepad");
            }
        } 
    }

    public void PlayerHasJoined(string input)
    {
        if (_hasP1Joined == false)
        {
            _levelManager.NextLevel();
            _inputP1.SetActive(false);
            _player1.SetActive(true);
            _hasP1Joined = true;
            _player1.GetComponent<LifeManager>().Init();
            _player1.GetComponentInChildren<Combat>().Init();
            if (input == "Keyboard&Mouse")
            {
                _player1.GetComponent<PlayerInput>().SwitchCurrentControlScheme(Keyboard.current);
                _keyboardInput = true;
            }

            if (input == "Gamepad")
            {
                _player1.GetComponent<PlayerInput>().SwitchCurrentControlScheme(Gamepad.current);
                _gamepadInput = true;
            }
            _P1Scheme = _player1.GetComponent<PlayerInput>().currentControlScheme;
            return;
        }
        if (_hasP2Joined == false && _hasP1Joined)
        {
            _player2.SetActive(true);
            _hasP2Joined = true;
            _inputP2.SetActive(false);
            _player2.GetComponent<LifeManager>().Init();
            _player2.GetComponentInChildren<Combat>().Init();
            
            if (input == "Keyboard&Mouse")
            {
                _player2.GetComponent<PlayerInput>().SwitchCurrentControlScheme(Keyboard.current);
                _keyboardInput = true;
            }

            if (input == "Gamepad")
            {
                _player2.GetComponent<PlayerInput>().SwitchCurrentControlScheme(Gamepad.current);
                _gamepadInput = true;
            }

            _P2Scheme = _player2.GetComponent<PlayerInput>().currentControlScheme;
        }
    }

    public void GameOverInit()
    {
        if (_player1.GetComponent<LifeManager>()._gameOver == true &&
            (_player2.GetComponent<LifeManager>()._gameOver == true || _hasP2Joined == false))
        {
            StartCoroutine(GameOver());
        }
    }

    private IEnumerator GameOver()
    {
        _waveTitle.SetActive(true);
        _waveTitleText.text = "Game Over";
        yield return _gameOverWait;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    /*public void P1HasJoined()
    {
        _hasP1Joined = true;
        _player1.SetActive(true);
        _levelManager.NextLevel();
    }

    public void P2HasJoined()
    {
        _hasP2Joined = true;
        _player2.SetActive(true);
    }*/
}
