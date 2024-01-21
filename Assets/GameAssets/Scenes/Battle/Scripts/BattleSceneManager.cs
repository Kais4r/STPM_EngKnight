using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    public BattleDataManager _battleDataManager;
    public BattleUIManager _battleUIManager;

    private bool _enemyTurn = true;

    private void Start()
    {
        StartCoroutine(_battleUIManager.StartCombatDialog());
    }
    private void Update()
    {
        if (_enemyTurn == false)
        {
            _enemyTurn = true;
            _battleDataManager.GenerateEnglishWord();
        }
    }
}