using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    
    [SerializeField] private PlayerManager _playerManager;

    [SerializeField] private SpawnManager _spawnManager;
    
    [SerializeField] public int _lifeCount;
    
    [SerializeField] private GameObject[] _lifeIcons;
    
    [SerializeField] private string _playerNumber;
    
    [SerializeField] public bool _gameOver;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _player = gameObject;
        _spawnManager = GameObject.Find("Spawner Manager").GetComponent<SpawnManager>();
        _playerManager = GameObject.Find("Player Manager").GetComponent<PlayerManager>();
        _gameOver = false; 
        if (_player.tag == "Player1")
        {
            _playerNumber = "P1";
        }
        if (_player.tag == "Player2")
        {
            _playerNumber = "P2";
        }

        _lifeIcons[0] = GameObject.Find(_playerNumber + "Life1");
        _lifeIcons[1] = GameObject.Find(_playerNumber + "Life2");
        _lifeIcons[2] = GameObject.Find(_playerNumber + "Life3");
        _lifeIcons[3] = GameObject.Find(_playerNumber + "Life4");
        _lifeIcons[0].SetActive(false);
        _lifeIcons[1].SetActive(false);
        _lifeIcons[2].SetActive(false);
        _lifeIcons[3].SetActive(false);
    }

    public void Init()
    {
        _lifeCount = 2;
        _lifeIcons[0].SetActive(true);
        _lifeIcons[1].SetActive(true);
    }

    public void LifeUp()
    {
        if (_lifeCount == 0)
        {
            _lifeIcons[0].SetActive(true);
            _lifeCount++;
            _spawnManager._currentLife++;
            return;
        }

        if (_lifeCount == 1)
        {
            _lifeIcons[1].SetActive(true);
            _lifeCount++;
            _spawnManager._currentLife++;
            return;
        }

        if (_lifeCount == 2)
        {
            _lifeIcons[2].SetActive(true);
            _lifeCount++;
            _spawnManager._currentLife++;
            return;
        }

        if (_lifeCount == 3)
        {
            _lifeIcons[3].SetActive(true);
            _lifeCount++;
            _spawnManager._currentLife++;
            return;
        }

        if (_lifeCount > 3)
        {
            _spawnManager._currentLife++;
            _lifeCount++;
        }
    }

    public void LifeDown()
    {
        if (_lifeCount == 0)
        {
            _gameOver = true;
            _playerManager.GameOverInit();
            _lifeCount--;
            return;
        }
        if (_lifeCount > 4)
        {
            _lifeCount--;
            _spawnManager._currentLife--;
        }
        if (_lifeCount == 4)
        {
            _lifeIcons[3].SetActive(false);
            _lifeCount--;
            _spawnManager._currentLife--;
            return;
        }
        if (_lifeCount == 3)
        {
            _lifeIcons[2].SetActive(false);
            _lifeCount--;
            _spawnManager._currentLife--;
            return;
        }
        if (_lifeCount == 2)
        {
            _lifeIcons[1].SetActive(false);
            _lifeCount--;
            _spawnManager._currentLife--;
            return;
        }
        if (_lifeCount == 1)
        {
            _lifeIcons[0].SetActive(false);
            _lifeCount--;
            _spawnManager._currentLife--;
            return;
        }
        
    }

    


    // public void LifeUp()
    // {
    //     if (_lifeCount == 0)
    //     {
    //         if (_player.tag == "Player1")
    //         {
    //             GameObject.FindGameObjectsWithTag("Player1Life")[0].SetActive(true);
    //         }
    //
    //         if (_player.tag == "Player2")
    //         {
    //             GameObject.FindGameObjectsWithTag("Player2Life")[0].SetActive(true);
    //
    //         }
    //         return;
    //     }
    //     if (_lifeCount == 1)
    //     {
    //         if (_player.tag == "Player1")
    //         {
    //             GameObject.FindGameObjectsWithTag("Player1Life")[1].SetActive(true);
    //         }
    //
    //         if (_player.tag == "Player2")
    //         {
    //             GameObject.FindGameObjectsWithTag("Player2Life")[1].SetActive(true);
    //
    //         }
    //         return;
    //     }
    //     if (_lifeCount == 2)
    //     {
    //         if (_player.tag == "Player1")
    //         {
    //             GameObject.FindGameObjectsWithTag("Player1Life")[2].SetActive(true);
    //         }
    //
    //         if (_player.tag == "Player2")
    //         {
    //             GameObject.FindGameObjectsWithTag("Player2Life")[2].SetActive(true);
    //
    //         }
    //         return;
    //     }
    //     if (_lifeCount == 3)
    //     {
    //         if (_player.tag == "Player1")
    //         {
    //             GameObject.FindGameObjectsWithTag("Player1Life")[3].SetActive(true);
    //         }
    //
    //         if (_player.tag == "Player2")
    //         {
    //             GameObject.FindGameObjectsWithTag("Player2Life")[3].SetActive(true);
    //
    //         }
    //         return;
    //     }
    //
    //     _lifeCount++;
    //
    // }
    //
    // public void PlayerDeath()
    // {
    //     if (_lifeCount == 1)
    //     {
    //         if (_player.tag == "Player1")
    //         {
    //             GameObject.FindGameObjectsWithTag("Player1Life")[0].SetActive(false);
    //         }
    //
    //         if (_player.tag == "Player2")
    //         {
    //             GameObject.FindGameObjectsWithTag("Player2Life")[0].SetActive(false);
    //
    //         }
    //         return;
    //     }
    //     if (_lifeCount == 2)
    //     {
    //         if (_player.tag == "Player1")
    //         {
    //             GameObject.FindGameObjectsWithTag("Player1Life")[1].SetActive(false);
    //         }
    //
    //         if (_player.tag == "Player2")
    //         {
    //             GameObject.FindGameObjectsWithTag("Player2Life")[1].SetActive(false);
    //
    //         }
    //         return;
    //     }
    //     if (_lifeCount == 3)
    //     {
    //         if (_player.tag == "Player1")
    //         {
    //             GameObject.FindGameObjectsWithTag("Player1Life")[2].SetActive(false);
    //         }
    //
    //         if (_player.tag == "Player2")
    //         {
    //             GameObject.FindGameObjectsWithTag("Player2Life")[2].SetActive(false);
    //
    //         }
    //         return;
    //     }
    //     if (_lifeCount == 4)
    //     {
    //         if (_player.tag == "Player1")
    //         {
    //             GameObject.FindGameObjectsWithTag("Player1Life")[3].SetActive(false);
    //         }
    //
    //         if (_player.tag == "Player2")
    //         {
    //             GameObject.FindGameObjectsWithTag("Player2Life")[3].SetActive(false);
    //
    //         }
    //         return;
    //         
    //     }
    //     _lifeCount--;
    //
    // }
}
