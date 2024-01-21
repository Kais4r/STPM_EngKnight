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

    //Characterinfo panel
    public TextMeshProUGUI playerName;

    public TextMeshProUGUI enemyName;

    //CharacterChatPanel
    public TextMeshProUGUI enemyChat;
    public TextMeshProUGUI playerChat;

    //PlayerAnswerPanel
    public List<TextMeshProUGUI> answerButtons;

    public void UpdateQuestionAndAnswer(EnglishWord wordToGuess)
    {
        enemyChat.text = wordToGuess.WordName;
    }
}
