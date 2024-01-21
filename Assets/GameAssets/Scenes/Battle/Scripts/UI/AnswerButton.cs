using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    [SerializeField] private BattleSceneManager _battleSceneManager;
    [SerializeField] private TextMeshProUGUI _answerText;
    private bool _result;

    private Button _answerButton;
    public void Awake()
    {
        _answerButton = gameObject.GetComponent<Button>();
        if (_answerButton == null )
        {
            Debug.LogError("answer button is null");
        }
    }

    public void SelectAnswer()
    {
        if(_battleSceneManager.battleState == BattleState.PlayerTurn)
        {
            _result = CheckAnswer();
            if (_battleSceneManager.gameMode == GameMode.EngLishToViet)
            {
                _battleSceneManager.ProcessPlayerQuizResult(_result);
            }
            // this is what happen after player select answer
            _battleSceneManager.battleState = BattleState.SystemProcessPlayerTurnResult;
        }
    }

    private bool CheckAnswer()
    {
        // will be differnt for differnt game mode
        if (_battleSceneManager.gameMode == GameMode.EngLishToViet)
        {
            if (_answerText.text == _battleSceneManager._battleDataManager.WordToGuess.VietMeaning)
            {
                StartCoroutine(ChangeButtonColor(new Color32(77, 255, 0, 255)));
                return true;
            }
            else
            {
                StartCoroutine(ChangeButtonColor(new Color32(255, 105, 105, 255)));
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

    private IEnumerator ChangeButtonColor(Color32 colorValue)
    {
        ColorBlock cb = _answerButton.colors;
        cb.selectedColor = colorValue;
        _answerButton.colors = cb;
        yield return new WaitForSeconds(.5f);
        cb.selectedColor = new Color32(255, 255, 255, 255);
        _answerButton.colors = cb;
    }
}
