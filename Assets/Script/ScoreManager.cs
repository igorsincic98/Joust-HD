using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private float _comboCooldown;
    [SerializeField] private int _lifeMilestone;
    [SerializeField] private LifeManager _lifeManager;
    [SerializeField] private int _combo;
    [SerializeField] private GameObject _scorePopUp;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private TextMeshProUGUI _scorePopUpText;
    [SerializeField] private Color _P1Color;
    [SerializeField] private Color _P2Color;

    [SerializeField] private WaitForSeconds _scoreLifeTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _scoreLifeTime = new WaitForSeconds(2f);
        _comboCooldown = 0f;
        _scoreText = gameObject.GetComponent<TextMeshProUGUI>();
        _lifeMilestone = 10000;
        _canvas = GameObject.Find("Canvas Foreground");
        if (gameObject.tag == "Player1")
        {
            _lifeManager = GameObject.Find("Player 1").GetComponent<LifeManager>();
        }

        if (gameObject.tag == "Player2")
        {
            _lifeManager = GameObject.Find("Player 2").GetComponent<LifeManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_comboCooldown > 0f)
        {
            _comboCooldown -= Time.deltaTime;
        }

        if (_comboCooldown <= 0f)
        {
            _combo = 0;
        }

        if (_score >= _lifeMilestone)
        {
            _lifeMilestone =+ 10000;
            _lifeManager.LifeUp();
        }
        _scoreText.text = _score.ToString();
    }

    public void EnemyScore(string _type, GameObject _player)
    {
        StartCoroutine(EnemyScoreProcess(_type, _player));
    }
    public IEnumerator EnemyScoreProcess(string _type, GameObject _player)
    {
        if (_type == "Bounder")
        {
            _score += 500;
            GameObject _scoreNotification = Instantiate(_scorePopUp, new Vector2(_player.transform.position.x, _player.transform.position.y + 1f), Quaternion.identity, _canvas.transform);
            _scoreNotification.GetComponent<TextMeshProUGUI>().text = "500";
            _scoreNotification.GetComponent<TextMeshProUGUI>().color = _player.GetComponent<SpriteRenderer>().color;
            yield return _scoreLifeTime;
            Destroy(_scoreNotification);
        }
        else if (_type == "Hunter")
        {
            _score += 750;
            GameObject _scoreNotification = Instantiate(_scorePopUp, new Vector2(_player.transform.position.x, _player.transform.position.y + 1f), Quaternion.identity, _canvas.transform);
            _scoreNotification.GetComponent<TextMeshProUGUI>().text = "750";
            _scoreNotification.GetComponent<TextMeshProUGUI>().color = _player.GetComponent<SpriteRenderer>().color;
            yield return _scoreLifeTime;
            Destroy(_scoreNotification);
        }
        else if (_type == "ShadowLord")
        {
            _score += 1500;
            GameObject _scoreNotification = Instantiate(_scorePopUp, new Vector2(_player.transform.position.x, _player.transform.position.y + 1f), Quaternion.identity, _canvas.transform);
            _scoreNotification.GetComponent<TextMeshProUGUI>().text = "1500";
            _scoreNotification.GetComponent<TextMeshProUGUI>().color = _player.GetComponent<SpriteRenderer>().color;
            yield return _scoreLifeTime;
            Destroy(_scoreNotification);
        }
        else if (_type == "Player1" || _type == "Player2")
        {
            _score += 2000;
            GameObject _scoreNotification = Instantiate(_scorePopUp, new Vector2(_player.transform.position.x, _player.transform.position.y + 1f), Quaternion.identity, _canvas.transform);
            _scoreNotification.GetComponent<TextMeshProUGUI>().text = "2000";
            _scoreNotification.GetComponent<TextMeshProUGUI>().color = _player.GetComponent<SpriteRenderer>().color;
            yield return _scoreLifeTime;
            Destroy(_scoreNotification);
        }
    }

    public void EggPickUp(GameObject _player)
    {
        StartCoroutine(EggPickUpProcess(_player));
    }

    public IEnumerator EggPickUpProcess(GameObject _player)
    {
        if (_comboCooldown <= 0f && _combo == 0)
        {
            _score += 100;
            _combo++;
            _comboCooldown = 5f;
            GameObject _scoreNotification = Instantiate(_scorePopUp, new Vector2(_player.transform.position.x, _player.transform.position.y + 1f), Quaternion.identity, _canvas.transform);
            _scoreNotification.GetComponent<TextMeshProUGUI>().text = "100";
            _scoreNotification.GetComponent<TextMeshProUGUI>().color = _player.GetComponent<SpriteRenderer>().color;
            yield return _scoreLifeTime;
            Destroy(_scoreNotification);
            yield break;
        }

        if ((_comboCooldown > 0f) && (_combo == 1))
        {
            _score += 250;
            _combo++;
            _comboCooldown = 5f;
            GameObject _scoreNotification = Instantiate(_scorePopUp, new Vector2(_player.transform.position.x, _player.transform.position.y + 1f), Quaternion.identity, _canvas.transform);
            _scoreNotification.GetComponent<TextMeshProUGUI>().text = "250";
            _scoreNotification.GetComponent<TextMeshProUGUI>().color = _player.GetComponent<SpriteRenderer>().color;
            yield return _scoreLifeTime;
            Destroy(_scoreNotification);
            yield break;
        }

        if ((_comboCooldown > 0f) && (_combo == 2))
        {
            _score += 500;
            _combo++;
            _comboCooldown = 5f;
            GameObject _scoreNotification = Instantiate(_scorePopUp, new Vector2(_player.transform.position.x, _player.transform.position.y + 1f), Quaternion.identity, _canvas.transform);
            _scoreNotification.GetComponent<TextMeshProUGUI>().text = "500";
            _scoreNotification.GetComponent<TextMeshProUGUI>().color = _player.GetComponent<SpriteRenderer>().color;
            yield return _scoreLifeTime;
            Destroy(_scoreNotification);
            yield break;
        }

        if ((_comboCooldown > 0f) && (_combo >= 3))
        {
            _score += 1000;
            _combo++;
            _comboCooldown = 5f;
            GameObject _scoreNotification = Instantiate(_scorePopUp, new Vector2(_player.transform.position.x, _player.transform.position.y + 1f), Quaternion.identity, _canvas.transform);
            _scoreNotification.GetComponent<TextMeshProUGUI>().text = "1000";
            _scoreNotification.GetComponent<TextMeshProUGUI>().color = _player.GetComponent<SpriteRenderer>().color;
            yield return _scoreLifeTime;
            Destroy(_scoreNotification);
            yield break;
        }
        
    }

    public void SurvivalWaveBonusInit(GameObject _player)
    {
        StartCoroutine(SurvivalWaveBonus(_player));
    }

    private IEnumerator SurvivalWaveBonus(GameObject _player)
    {
        _score += 2000;
        _combo++;
        _comboCooldown = 5f;
        GameObject _scoreNotification = Instantiate(_scorePopUp, new Vector2(_player.transform.position.x, _player.transform.position.y + 1f), Quaternion.identity, _canvas.transform);
        _scoreNotification.GetComponent<TextMeshProUGUI>().text = "2000";
        _scoreNotification.GetComponent<TextMeshProUGUI>().color = _player.GetComponent<SpriteRenderer>().color;
        yield return _scoreLifeTime;
        Destroy(_scoreNotification);
        yield break;

    }
}
