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
    public TextMeshProUGUI answerBtn1;
    public TextMeshProUGUI answerBtn2;
    public TextMeshProUGUI answerBtn3;
    public TextMeshProUGUI answerBtn4;

    public IEnumerator StartCombatDialog()
    {
        enemyChat.text = "E:let get started noob";
        yield return new WaitForSeconds(1f);
    }

    public void UpdateQuestionAndAnswer()
    {
        //enemyChat.text = 
    }
}
