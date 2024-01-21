using Newtonsoft.Json;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Xml;

public class BattleDataManager : MonoBehaviour
{
    private string jsonString;

    public List<EnglishWord> WordsList { get; set; }
    public List<EnglishWord> GeneratedWordsList { get; set; }
    
    public EnglishWord WordToGuess {  get; set; }
    public List<EnglishWord> WrongAnswerWordsList { get; set; }

    public void LoadData(string streamingAssetdataPath)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            StartCoroutine(LoadDataAndroid(streamingAssetdataPath));
        }
        else
        {
            jsonString = File.ReadAllText(streamingAssetdataPath);
            ProcessData();
        }
    }

    private IEnumerator LoadDataAndroid(string streamingAssetdataPath)
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
            ProcessData();
        }
    }

    private void ProcessData()
    {
        WordsList = JsonConvert.DeserializeObject<List<EnglishWord>>(jsonString);
    }

    // Generating word to guess and 3 wrong answer words function
    public void GenerateEnglishWordsList()
    {   
        if (WordsList.Count <= 5)
        {
            return;
        }
        else
        {
            // Generate Question Word and Correct Answer
            int randomIndex = Random.Range(0, WordsList.Count);
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
                        randomIndex = Random.Range(0, WordsList.Count);
                        randomWord = WordsList[randomIndex];
                    }
                }
            }

            randomIndex = Random.Range(0, WordsList.Count);
            randomWord = WordsList[randomIndex];
            WrongAnswerWordsList ??= new List<EnglishWord>()
            { 
                randomWord
            };
            for (int i = WrongAnswerWordsList.Count;i < 3;i++)
            {
                while (randomWord == WordToGuess || WrongAnswerWordsList.Contains(randomWord))
                {
                    randomIndex = Random.Range(0, WordsList.Count);
                    randomWord = WordsList[randomIndex];
                }
                WrongAnswerWordsList.Add(randomWord);
            }
        }
    }
}
