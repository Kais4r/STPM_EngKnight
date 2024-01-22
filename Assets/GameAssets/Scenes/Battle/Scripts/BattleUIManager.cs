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

    // GameEndPanel
    public GameObject endGamePanel;
    public TextMeshProUGUI endGameResultText;

    public void UpdateQuestionAndAnswer(EnglishWord wordToGuess)
    {
        enemyChat.text = wordToGuess.WordName;
    }

    public void SetUpCharacterInfo()
    {
        _player = _battleSceneManager.GetCharacter("player");
        _enemy = _battleSceneManager.GetCharacter("enemy");

        playerName.text = _player.Name;
        playerHP.text = _player.HP.ToString();

        enemyName.text = _enemy.Name;
        enemyHP.text = _enemy.HP.ToString();
    }

    public void UpdateCombatInfo()
    {
        _player = _battleSceneManager.GetCharacter("player");
        _enemy = _battleSceneManager.GetCharacter("enemy");

        playerHP.text = _player.HP.ToString();
        enemyHP.text = _enemy.HP.ToString();
    }
}
