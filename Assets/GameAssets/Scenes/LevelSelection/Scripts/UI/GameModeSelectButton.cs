using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeSelectButton : MonoBehaviour
{
    private GameManagerSingleton _gameManagerSingleton;
    [SerializeField] private GameMode _gameMode;

    private void Awake()
    {
        _gameManagerSingleton = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerSingleton>();
    }
    public void SelectGameMode(string sceneName)
    {
        _gameManagerSingleton.gameMode = _gameMode;
        SceneManager.LoadScene(sceneName);
    }
}
