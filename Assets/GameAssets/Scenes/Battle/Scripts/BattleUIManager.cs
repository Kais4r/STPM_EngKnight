using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    // game manager need to pass in data here
    [SerializeField] private BattleSceneManager _battleSceneManager;


    //Uppercontrol panel
    public TextMeshProUGUI scoreText;

    //Store character info
    private Character _player;
    private Character _enemy;

    //Characterinfo panel
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerHP;

    public TextMeshProUGUI enemyName;
    public TextMeshProUGUI enemyHP;

    //CharacterChatPanel
    public TextMeshProUGUI enemyChat;
    public TextMeshProUGUI playerChat;

    //PlayerAnswerPanel
    public List<TextMeshProUGUI> answerButtons;

    public void UpdateQuestionAndAnswer(EnglishWord wordToGuess)
    {
        enemyChat.text = wordToGuess.WordName;
    }

    public void SetUpCharacterInfo()
    {
        _player = _battleSceneManager.GetCharacter("player");
        _enemy = _battleSceneManager.GetCharacter("enemy");

        playerName.text = _player.Name;
        playerHP.text = "HP: " + _player.HP.ToString();

        enemyName.text = _enemy.Name;
        enemyHP.text = "HP: " + _enemy.HP.ToString();
    }

    public void UpdateCombatInfo()
    {
        _player = _battleSceneManager.GetCharacter("player");
        _enemy = _battleSceneManager.GetCharacter("enemy");

        playerHP.text = "HP: " + _player.HP.ToString();
        enemyHP.text = "HP: " + _enemy.HP.ToString();
    }
}
