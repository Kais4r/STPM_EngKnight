using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    private bool _buttonState = false;
    private GameManagerSingleton _gameManagerSingleton;
    private void Awake()
    {
        _gameManagerSingleton = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerSingleton>();
    }
    public void TurnOnOrOffPanel()
    {
        _gameManagerSingleton.cefrLevel = _textMeshProUGUI.text;

        if (_buttonState == false)
        {
            _panel.SetActive(true);
            _buttonState = true;
        }
        else
        {
            _panel.SetActive(false);
            _buttonState = false;
        }
    }
}
