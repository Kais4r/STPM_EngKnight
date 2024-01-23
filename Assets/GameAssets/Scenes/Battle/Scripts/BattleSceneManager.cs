using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
    EngLishToViet,
    VietToEnglish,
    EngLishDescriptionToEnglish,
    VietDescriptionToEnglish,
}

public class BattleSceneManager : MonoBehaviour
{
    [SerializeField] private Image backGroundImage;
    public List<Sprite> backGroundList;

    private GameManagerSingleton _gameManagerSingleton;
    public BattleState battleState = BattleState.BattleStart;
    public GameMode gameMode = GameMode.VietToEnglish;

    // This is where we pass in level database name
    // private string databaseName = "A1";
    public BattleDataManager _battleDataManager;
    private string streamingAssetdataPath;

    // UserInterface
    public BattleUIManager _battleUIManager;

    // Level Info
    private int remainingEnemiesNumber = 3;
    private int[] _range; //Words range
    public int hintLeft = 2;

    // Generate enemy and character
    private Character _player;
    private Character _enemy;

    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Animator _enemyAnimator;

    [SerializeField] private List<RuntimeAnimatorController> animatorControllers;

    private void Awake()
    {
        _gameManagerSingleton = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerSingleton>();

        if (_gameManagerSingleton.cefrLevel == "A1" || _gameManagerSingleton.cefrLevel == "A2")
        {
            backGroundImage.GetComponent<Image>().sprite = backGroundList[0];
        }
        else if (_gameManagerSingleton.cefrLevel == "B1" || _gameManagerSingleton.cefrLevel == "B2")
        {
            backGroundImage.GetComponent<Image>().sprite = backGroundList[1];
        }
        else
        {
            backGroundImage.GetComponent<Image>().sprite = backGroundList[2];
        }

        //streamingAssetdataPath = Application.streamingAssetsPath + "/Level/" + databaseName + ".json";
        streamingAssetdataPath = Application.streamingAssetsPath + "/Level/" + _gameManagerSingleton.cefrLevel + ".json";
        gameMode = _gameManagerSingleton.gameMode;
    }
    private void Update()
    {
        if (battleState == BattleState.BattleStart)
        {
            //set up database here:
            _range = new int[] { 0, 40 };
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
        _player = new("Player", 1, 5, 100, 0);
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
        else if (gameMode == GameMode.VietToEnglish)
        {
            SetUpVietToEnglishQuizQuestion();
        }
        else if (gameMode == GameMode.EngLishDescriptionToEnglish)
        {
            SetUpEnglishDesToEnglishQuizQuestion();
        }
        else if (gameMode == GameMode.VietDescriptionToEnglish)
        {
            SetUpVietDesToEnglishQuizQuestion();
        }
        battleState = BattleState.PlayerTurn;
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
            _enemyAnimator.runtimeAnimatorController = animatorControllers[6];
            // generate the boss
            character = new("Final boss", 1, 2, 100, 0);

        }
        else
        {
            int randomEnemyController = UnityEngine.Random.Range(2, 6);
            _enemyAnimator.runtimeAnimatorController = animatorControllers[randomEnemyController];
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
        _playerAnimator.Play("Base Layer.PlayerAttack",0,0);
        yield return new WaitForSeconds(1f);
        _playerAnimator.Play("Base Layer.Idle", 0, 0);
        _battleUIManager.UpdateCombatInfo();
        if (_enemy.HP == 0)
        {
            _enemyAnimator.Play("Base Layer.Dead", 0, 0);
            yield return new WaitForSeconds(0.5f);
            remainingEnemiesNumber--;
            if(remainingEnemiesNumber > 0)
            {
                battleState = BattleState.SpawnNextEnemy;
                _enemy = GenerateEnemy();
                yield return new WaitForSeconds(0.5f);
                _battleUIManager.SetUpCharacterInfo();
                battleState = BattleState.EnemyTurn;
            }
            else
            {
                _gameManagerSingleton.generatedWords = _battleDataManager.GeneratedWordsList;
                _battleUIManager.endGamePanel.SetActive(true);
                _battleUIManager.endGameResultText.text = "Victory!";
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
        _enemyAnimator.Play("Base Layer.EnemyAttack", 0, 0);
        yield return new WaitForSeconds(1f);
        _enemyAnimator.Play("Base Layer.Idle", 0, 0);
        _battleUIManager.UpdateCombatInfo();

        if (_player.HP <= 0)
        {
            _gameManagerSingleton.generatedWords = _battleDataManager.GeneratedWordsList;
            _battleUIManager.endGamePanel.SetActive(true);
            _battleUIManager.endGameResultText.text = "Defeated";
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            // If player still alive, enemy will ask back the player
            if(gameMode == GameMode.EngLishToViet)
            {
                SetUpEnglishToVietQuizQuestion();
            }
            else if(gameMode == GameMode.VietToEnglish)
            {
                SetUpVietToEnglishQuizQuestion();
            }
            else if(gameMode == GameMode.EngLishDescriptionToEnglish)
            {
                SetUpEnglishDesToEnglishQuizQuestion();
            }
            else if (gameMode == GameMode.VietDescriptionToEnglish)
            {
                SetUpVietDesToEnglishQuizQuestion();
            }
            battleState = BattleState.PlayerTurn;
        }
    }

    // Set up question functions:
    // English to Viet
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

    // Viet to English
    private void SetUpVietToEnglishQuizQuestion()
    {
        _battleDataManager.GenerateEnglishWordsList();
        _battleUIManager.enemyChat.text = "E:Từ này là gì: " + _battleDataManager.WordToGuess.VietMeaning;
        _battleUIManager.playerChat.text = "P:Hmm...";

        List<int> arr = new() { 0, 1, 2, 3 };
        System.Random random = new System.Random();
        arr = arr.OrderBy(x => random.Next()).ToList();

        _battleUIManager.answerButtons[arr[0]].text = _battleDataManager.WordToGuess.WordName;
        arr.RemoveAt(0);

        for (int i = 0; i < arr.Count; i++)
        {
            _battleUIManager.answerButtons[arr[i]].text = _battleDataManager.WrongAnswerWordsList[i].WordName;
        }
    }

    private void SetUpEnglishDesToEnglishQuizQuestion()
    {
        _battleDataManager.GenerateEnglishWordsList();
        _battleUIManager.enemyChat.text = "E:" + _battleDataManager.WordToGuess.EnglishDescription;
        _battleUIManager.playerChat.text = "P:Hmm...";

        List<int> arr = new() { 0, 1, 2, 3 };
        System.Random random = new System.Random();
        arr = arr.OrderBy(x => random.Next()).ToList();

        _battleUIManager.answerButtons[arr[0]].text = _battleDataManager.WordToGuess.WordName;
        arr.RemoveAt(0);

        for (int i = 0; i < arr.Count; i++)
        {
            _battleUIManager.answerButtons[arr[i]].text = _battleDataManager.WrongAnswerWordsList[i].WordName;
        }
    }

    private void SetUpVietDesToEnglishQuizQuestion()
    {
        _battleDataManager.GenerateEnglishWordsList();
        _battleUIManager.enemyChat.text = "E:" + _battleDataManager.WordToGuess.VietDescription;
        _battleUIManager.playerChat.text = "P:Hmm...";

        List<int> arr = new() { 0, 1, 2, 3 };
        System.Random random = new System.Random();
        arr = arr.OrderBy(x => random.Next()).ToList();

        _battleUIManager.answerButtons[arr[0]].text = _battleDataManager.WordToGuess.WordName;
        arr.RemoveAt(0);

        for (int i = 0; i < arr.Count; i++)
        {
            _battleUIManager.answerButtons[arr[i]].text = _battleDataManager.WrongAnswerWordsList[i].WordName;
        }
    }
}
