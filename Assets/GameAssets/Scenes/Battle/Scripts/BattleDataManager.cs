using Newtonsoft.Json;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Xml;
using System.Runtime.CompilerServices;
using System;

public class BattleDataManager : MonoBehaviour
{
    private GameManagerSingleton _gameManagerSingleton;
    [SerializeField] private BattleSceneManager _battleSceneManager;
    private string jsonString;

    public List<EnglishWord> WordsList { get; set; }
    public List<EnglishWord> GeneratedWordsList { get; set; }
    
    public EnglishWord WordToGuess {  get; set; }
    public List<EnglishWord> WrongAnswerWordsList { get; set; }

    public void Awake()
    {
        _gameManagerSingleton = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerSingleton>();
    }

    public void LoadData(string streamingAssetdataPath, int[] range)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            StartCoroutine(LoadDataAndroid(streamingAssetdataPath, range));
        }
        else
        {
            jsonString = File.ReadAllText(streamingAssetdataPath);
            ProcessData(range);
        }
    }

    private IEnumerator LoadDataAndroid(string streamingAssetdataPath, int[] range)
    {
        // Create a UnityWebRequest to load the file
        UnityWebRequest www = UnityWebRequest.Get(streamingAssetdataPath);

        // Send the request and wait for it to finish
        yield return www.SendWebRequest();

        // Check for any errors during the request
        if (www.result == UnityWebRequest.Result.Success)
        {
            // Get the downloaded text
            jsonString = www.downloadHandler.text;
            ProcessData(range);
        }
    }

    private void ProcessData(int[] range)
    {
        List<EnglishWord> data = JsonConvert.DeserializeObject<List<EnglishWord>>(jsonString);
        WordsList = data.GetRange(range[0], range[1]);
    }

    public void GenerateEnglishWordsList()
    {   
        if (WordsList.Count <= 0)
        {
            _gameManagerSingleton.generatedWords = GeneratedWordsList;
            _battleSceneManager._battleUIManager.endGamePanel.SetActive(true);
            _battleSceneManager._battleUIManager.endGameResultText.text = "Defeated, out of words";
        }
        else
        {
            // Generate Question Word and Correct Answer
            int randomIndex = UnityEngine.Random.Range(0, WordsList.Count);
            EnglishWord randomWord = WordsList[randomIndex];
            if(GeneratedWordsList == null)
            {
                WordToGuess = randomWord;
                GeneratedWordsList = new List<EnglishWord>
                {
                    randomWord
                };
                WordsList.RemoveAt(randomIndex);
            }
            else
            {
                if (GeneratedWordsList.Contains(randomWord) == false)
                {
                    //assign the correct word that player has to guess
                    WordToGuess = randomWord;
                    GeneratedWordsList.Add(randomWord);
                    WordsList.RemoveAt(randomIndex);
                }
                else
                {
                    while (GeneratedWordsList.Contains(randomWord) == true)
                    {
                        randomIndex = UnityEngine.Random.Range(0, WordsList.Count);
                        randomWord = WordsList[randomIndex];
                    }
                }
            }

            randomIndex = UnityEngine.Random.Range(0, WordsList.Count);
            randomWord = WordsList[randomIndex];
            if (WrongAnswerWordsList == null)
            {
                while(randomWord == WordToGuess)
                {
                    randomIndex = UnityEngine.Random.Range(0, WordsList.Count);
                    randomWord = WordsList[randomIndex];
                }
                WrongAnswerWordsList = new List<EnglishWord> {  randomWord };
            }
            else
            {
                WrongAnswerWordsList.Clear();
                while (randomWord == WordToGuess)
                {
                    randomIndex = UnityEngine.Random.Range(0, WordsList.Count);
                    randomWord = WordsList[randomIndex];
                }
                WrongAnswerWordsList = new List<EnglishWord> { randomWord };
            }
            for (int i = WrongAnswerWordsList.Count;i < 3;i++)
            {
                while (randomWord == WordToGuess || WrongAnswerWordsList.Contains(randomWord))
                {
                    randomIndex = UnityEngine.Random.Range(0, WordsList.Count);
                    randomWord = WordsList[randomIndex];
                }
                WrongAnswerWordsList.Add(randomWord);
            }
        }
    }
}
