using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _level;
    
    [SerializeField] private SpawnManager _spawnManager;

    [SerializeField] private GameObject _platform1;
    
    [SerializeField] private GameObject _platform2;
    
    [SerializeField] private GameObject _platform3;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spawnManager = GameObject.Find("Spawner Manager").GetComponent<SpawnManager>();
        _level = 0;
        _platform1 = GameObject.Find("TM Platform 1");
        _platform2 = GameObject.Find("TM Platform 2");
        _platform3 = GameObject.Find("TM Platform 3");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLevel()
    {
        _level++;
        if ((_level - 2) % 5 == 0)
        {
            _spawnManager._survivalWave = true;
        }
        if (_level == 1)
        {
            Debug.Log("Level " +_level + " is loading");
            _spawnManager._waveInfo = "Vague " + _level;
            _spawnManager._bounderCount = 4;
            _spawnManager.InitEnemySpawn();
            return;
        }

        if (_level == 2)
        {
            Debug.Log("Level " + _level + " is loading");
            _spawnManager._waveInfo = "Vague " + _level;
            _spawnManager._bounderCount = 5;
            _spawnManager.InitEnemySpawn();
            return;
        }

        if (_level == 3)
        {
            Debug.Log("Level " + _level + " is loading");
            _spawnManager._waveInfo = "Vague " + _level;
            _spawnManager._bounderCount = 6;
            _spawnManager.InitEnemySpawn();
            return;
        }

        if (_level == 4 || _level == 6)
        {
            Debug.Log("Level " + _level + " is loading");
            _spawnManager._waveInfo = "Vague " + _level;
            _spawnManager._bounderCount = 3;
            _spawnManager._hunterCount = 3;
            _spawnManager.InitEnemySpawn();
            return;
        }

        if (_level % 5 == 0)
        {
            Debug.Log("Level " + _level + " is loading");
            _spawnManager._waveInfo = "Vague " + _level + " - Vague d'oeufs";
            _spawnManager.InitEggWave();
            _platform1.SetActive(true);
            _platform2.SetActive(true);
            _platform3.SetActive(true);
            return;
        }
        
        if (_level == 7)
        {
            Debug.Log("Level " + _level + " is loading");
            _spawnManager._waveInfo = "Vague " + _level;
            _spawnManager._bounderCount = 2;
            _spawnManager._hunterCount = 4;
            _platform1.SetActive(false);
            _spawnManager.InitEnemySpawn();
            return;
        }

        if (_level == 8 || _level == 9 )
        {
            Debug.Log("Level " + _level + " is loading");
            _spawnManager._waveInfo = "Vague " + _level;
            _spawnManager._hunterCount = 6;
            if (_level == 8)
            {
                _platform1.SetActive(false);
            }

            if (_level == 9)
            {
                _platform2.SetActive(false);
            }
            _spawnManager.InitEnemySpawn();
            return;
        }
        if (_level == 11)
        {
            Debug.Log("Level " + _level + " is loading");
            _spawnManager._waveInfo = "Vague " + _level;
            _spawnManager._bounderCount = 3;
            _spawnManager._hunterCount = 4;
            _spawnManager.InitEnemySpawn();
            return;
        }
        if (_level == 12)
        {
            Debug.Log("Level " + _level + " is loading");
            _spawnManager._waveInfo = "Vague " + _level;
            _spawnManager._bounderCount = 2;
            _spawnManager._hunterCount = 6;
            _platform1.SetActive(false);
            _spawnManager.InitEnemySpawn();
            return;
        }
        if (_level == 13)
        {
            Debug.Log("Level " + _level + " is loading");
            _spawnManager._waveInfo = "Vague " + _level;
            _spawnManager._hunterCount = 7;
            _platform2.SetActive(false);
            _spawnManager.InitEnemySpawn();
            return;
        }
        if (_level == 14)
        {
            Debug.Log("Level " + _level + " is loading");
            _spawnManager._waveInfo = "Vague " + _level;
            _spawnManager._hunterCount = 8;
            _platform3.SetActive(false);
            _spawnManager.InitEnemySpawn();
            return;
        }
        if (_level == 16 || _level == 17)
        {
            Debug.Log("Level " + _level + " is loading");
            _spawnManager._waveInfo = "Vague " + _level;
            _spawnManager._hunterCount = 5;
            _spawnManager._shadowLordCount = 1;
            if (_level == 17)
            {
                _platform1.SetActive(false);
            }
            _spawnManager.InitEnemySpawn();
            return;
        }

        if (_level == 18)
        {
            Debug.Log("Level " + _level + " is loading");
            _spawnManager._waveInfo = "Vague " + _level;
            _spawnManager._hunterCount = 4;
            _spawnManager._shadowLordCount = 2;
            _platform2.SetActive(false);
            _spawnManager.InitEnemySpawn();
        }

        if (_level == 19)
        {
            Debug.Log("Level " + _level + " is loading");
            _spawnManager._waveInfo = "Vague " + _level;
            _spawnManager._hunterCount = 3;
            _spawnManager._shadowLordCount = 3;
            _platform3.SetActive(false);
            _spawnManager.InitEnemySpawn();
        }

        if (_level == 21)
        {
            Debug.Log("Level " + _level + " is loading");
            _spawnManager._waveInfo = "Fin de la partie";
        }
    }
}
