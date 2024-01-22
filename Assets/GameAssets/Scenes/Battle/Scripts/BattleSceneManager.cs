using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.VFX;

public enum BattleState
{
    BattleStart,
    DialogRunning,
    EnemyTurn,
    PlayerTurn,
    SystemProcessPlayerTurnResult,
    SpawnNextEnemy,
    CharacterAction
}

public enum GameMode
{
    EngLishToViet
}

public class BattleSceneManager : MonoBehaviour
{
    public BattleState battleState = BattleState.BattleStart;
    public GameMode gameMode = GameMode.EngLishToViet;

    // This is where we pass in level database name
    [SerializeField] private string databaseName;
    public BattleDataManager _battleDataManager;
    private string streamingAssetdataPath;

    // UserInterface
    public BattleUIManager _battleUIManager;

    // Level Info
    // public int enemiesNumber = 5;

    [SerializeField] private Animator _playerAnimController;

    private void Awake()
    {
        streamingAssetdataPath = Application.streamingAssetsPath + "/Level/" + databaseName + ".json";
    }
    private void Update()
    {
        if(battleState == BattleState.BattleStart)
        {
            //set up database here:
            _battleDataManager.LoadData(streamingAssetdataPath);

            // this will decide who attack first pass from level info, not implement yet
            battleState = BattleState.DialogRunning;
            _battleUIManager.enemyChat.text = "E:Let fight noob";

            StartCoroutine(SetUpBattle());
        }
        /*if (battleState == BattleState.EnemyTurn)
        {

        }*/
    }

    private IEnumerator SetUpBattle()
    {
        // wait for dialog running
        yield return new WaitForSeconds(1f);

        // Set up question depend of game mode
        if (gameMode == GameMode.EngLishToViet)
        {
            SetUpEnglishToVietQuizQuestion();
        }
        battleState = BattleState.PlayerTurn;
    }

    // Set up question functions:
    private void SetUpEnglishToVietQuizQuestion()
    {
        _battleDataManager.GenerateEnglishWordsList();
        _battleUIManager.enemyChat.text = "E:Tell me what is " + _battleDataManager.WordToGuess.WordName;
        _battleUIManager.playerChat.text = "P:Hmm...";

        List<int> arr = new() { 0, 1, 2, 3 };
        System.Random random = new System.Random();
        arr = arr.OrderBy(x => random.Next()).ToList();

        _battleUIManager.answerButtons[arr[0]].text = _battleDataManager.WordToGuess.VietMeaning;
        arr.RemoveAt(0);

        for (int i = 0; i < arr.Count; i++)
        {
            _battleUIManager.answerButtons[arr[i]].text = _battleDataManager.WrongAnswerWordsList[i].VietMeaning;
        }
    }

    public void ProcessPlayerQuizResult(bool result)
    {
        battleState = BattleState.CharacterAction;
        if (result == true)
        {
            Debug.Log("Player attack");
            StartCoroutine(PlayAttackAnimation(_playerAnimController, "Base Layer.PlayerAttack", BattleState.EnemyTurn,1f));
            // PlayerAttack()
        }
        else
        {
            Debug.Log("Enemy attack");
            StartCoroutine(PlayAttackAnimation(_playerAnimController, "Base Layer.PlayerAttack", BattleState.EnemyTurn, 1f));
            // EnemyAttack()
        }
    }

    private IEnumerator PlayAttackAnimation(Animator animator, string animation, BattleState battleStateToChange, float duration)
    {
        animator.Play(animation,0,0);
        yield return new WaitForSeconds(duration);
        // i put this line here because if you put this in ProcessPlauerQuizResult, it won't wait and play instantly
        animator.Play("Base Layer.Idle", 0, 0);
        battleState = battleStateToChange;
    }
}