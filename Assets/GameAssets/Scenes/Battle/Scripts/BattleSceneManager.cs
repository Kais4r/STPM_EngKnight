using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum BattleState
{
    BattleStart,
    DialogRunning,
    EnemyTurn,
    PlayerTurn,
    SystemProcessPlayerTurnResult,
    CharacterAction,
    SpawnNextEnemy,
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
    private int remainingEnemiesNumber = 3;
    private int[] _range;

    // Generate enemy and character
    private Character _player;
    private Character _enemy;

    [SerializeField] private Animator _playerAnimController;
    [SerializeField] private Animator _enemyAnimController;
    [SerializeField] private Color _playerIamgeColor;
    [SerializeField] private Color _enemyImageColor;

    private void Awake()
    {
        streamingAssetdataPath = Application.streamingAssetsPath + "/Level/" + databaseName + ".json";
    }
    private void Update()
    {
        if (battleState == BattleState.BattleStart)
        {
            //set up database here:
            _range = new int[] { 0, 20 };
            _battleDataManager.LoadData(streamingAssetdataPath,_range);
            StartCoroutine(SetUpBattle());
        }
        if (battleState == BattleState.EnemyTurn)
        {
            StartCoroutine(EnemyAttack());
        }
    }

    private IEnumerator SetUpBattle()
    {
        // Set up character
        _player = new("Khoinoob", 1, 5, 100, 0);
        _enemy = GenerateEnemy();
        _battleUIManager.SetUpCharacterInfo();

        // this will decide who attack first pass from level info, not implement yet
        battleState = BattleState.DialogRunning;
        _battleUIManager.enemyChat.text = "E:Let fight noob";

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
        // Tra loi dung
        if (result == true)
        {


            StartCoroutine(PlayerAttack());
        }
        // Tra loi sai
        else
        {
            battleState = BattleState.EnemyTurn;
        }
    }

    private Character GenerateEnemy()
    {
        Character character;
        if (remainingEnemiesNumber == 1)
        {
            // generate the boss
            character = new("Final boss", 1, 2, 100, 0);

        }
        else
        {
            character = new("Enemy[" + remainingEnemiesNumber + "]", 1, 1, 100, 0);
        }
        return character;
    }

    public Character GetCharacter(string target)
    {
        Character result;
        if (target == "player")
        {
            result = _player;
        }
        else if (target == "enemy")
        {
            result = _enemy;
        }
        else
        {
            result = new("error getting character", 1, 1, 100, 0);
        }
        return result;
    }
    private IEnumerator PlayerAttack()
    {
        _player.AttackCharacter(_enemy);
        _playerAnimController.Play("Base Layer.PlayerAttack",1,0);
        yield return new WaitForSeconds(1f);
        _playerAnimController.Play("Base Layer.Idle", 0, 0);
        _battleUIManager.UpdateCombatInfo();
        if (_enemy.HP == 0)
        {
            remainingEnemiesNumber--;
            if(remainingEnemiesNumber > 0)
            {
                battleState = BattleState.SpawnNextEnemy;
                _enemy = GenerateEnemy();
                yield return new WaitForSeconds(0.75f);
                _battleUIManager.SetUpCharacterInfo();
                battleState = BattleState.EnemyTurn;
            }
            else
            {
                _battleUIManager.endGamePanel.SetActive(true);
                _battleUIManager.endGameResultText.text = "Victory, you slain all monster!";
            }
        }
        else
        {
            battleState = BattleState.EnemyTurn;
        }

    }

    private IEnumerator EnemyAttack()
    {
        battleState = BattleState.CharacterAction;
        _enemy.AttackCharacter(_player);
        _enemyAnimController.Play("Base Layer.EmemyAttack", 1, 0);
        yield return new WaitForSeconds(1f);
        _enemyAnimController.Play("Base Layer.Idle", 0, 0);
        _battleUIManager.UpdateCombatInfo();

        if (_player.HP <= 0)
        {
            _battleUIManager.endGamePanel.SetActive(true);
            _battleUIManager.endGameResultText.text = "Defeated, The monster are scary";
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            // If player still alive, enemy will ask back the player
            if(gameMode == GameMode.EngLishToViet)
            {
                SetUpEnglishToVietQuizQuestion();
                battleState = BattleState.PlayerTurn;
            }
        }
    }
}
