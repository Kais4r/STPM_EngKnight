using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    [SerializeField] private BattleSceneManager _battleSceneManager;
    [SerializeField] private TextMeshProUGUI _answerText;
    [HideInInspector] TextMeshProUGUI enemyText;
    [HideInInspector] TextMeshProUGUI playerText;


    private bool _result;

    private Button _answerButton;
    public void Awake()
    {
        _answerButton = gameObject.GetComponent<Button>();
        if (_answerButton == null )
        {
            Debug.LogError("answer button is null");
        }
        enemyText = _battleSceneManager._battleUIManager.enemyChat;
        playerText = _battleSceneManager._battleUIManager.playerChat;
    }
    public void SelectAnswer()
    {
        if (_battleSceneManager.gameMode == GameMode.EngLishToViet)
        {
            if (_battleSceneManager.battleState == BattleState.PlayerTurn)
            {
                playerText.text = "P:It is " + _answerText.text + " ";
                _battleSceneManager.battleState = BattleState.SystemProcessPlayerTurnResult;
                _result = CheckAnswer();
                _battleSceneManager.ProcessPlayerQuizResult(_result);
                // this is what happen after player select answer 
            }
        }
    }

    private bool CheckAnswer()
    {
        // will be differnt for differnt game mode
        if (_battleSceneManager.gameMode == GameMode.EngLishToViet)
        {
            string correctVietMeaning = _battleSceneManager._battleDataManager.WordToGuess.VietMeaning;
            // right answer
            if (_answerText.text == correctVietMeaning)
            {
                StartCoroutine(ShowAnswer(new Color32(77, 255, 0, 255), enemyText, "E:Correct."));
                return true;
            }
            else
            {
                StartCoroutine(ShowAnswer(new Color32(255, 105, 105, 255), enemyText, "E:It is: " + correctVietMeaning));
                return false;

                // !!! code to write: use unity event to trigger if the False answer is selected, the button with the right answer go green for 0.5 second;
            }
        }
        else
        {
            Debug.LogError("AnswerButton.cs,CheckAnswer(): cant check answer");
            return false;
        }
    }

    private IEnumerator ShowAnswer(Color32 colorValue, TextMeshProUGUI textMeshProUGUI, string textToShow)
    {
        ColorBlock cb = _answerButton.colors;
        cb.selectedColor = colorValue;
        _answerButton.colors = cb;
        yield return new WaitForSeconds(0.5f);
        cb.selectedColor = new Color32(255, 255, 255, 255);
        _answerButton.colors = cb;

        textMeshProUGUI.text = textToShow;
    }

}
