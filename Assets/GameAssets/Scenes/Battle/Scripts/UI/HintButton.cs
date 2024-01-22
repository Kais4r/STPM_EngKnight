using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class HintButton : MonoBehaviour
{
    [SerializeField] private BattleSceneManager _battleSceneManager;
    [SerializeField] private TextMeshProUGUI hintButtonText;
    public GameObject hintPanel;
    public TextMeshProUGUI hintText;

    public void Awake()
    {
        hintButtonText.text = _battleSceneManager.hintLeft.ToString();
    }
    public void ShowHint()
    {
        if(_battleSceneManager.battleState == BattleState.PlayerTurn && _battleSceneManager.hintLeft > 0)
        {
            hintPanel.SetActive(true);

            if (_battleSceneManager.gameMode == GameMode.EngLishToViet)
            {
                hintText.text = "Hint: Hai chữ cái đầu của chữ cần đoán là: " + _battleSceneManager._battleDataManager.WordToGuess.VietMeaning.Substring(0, 2);
            }
            else
            {
                hintText.text = "Hint: the first two letter of the word is: " + _battleSceneManager._battleDataManager.WordToGuess.WordName.Substring(0, 2);
            }
            _battleSceneManager.hintLeft--;
            hintButtonText.text = _battleSceneManager.hintLeft.ToString();
        }
    }
}
