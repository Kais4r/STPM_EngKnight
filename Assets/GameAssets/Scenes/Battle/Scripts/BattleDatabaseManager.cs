using Newtonsoft.Json;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class BattleDatabaseManager : MonoBehaviour
{
    [SerializeField] private string databaseName;
    private string streamingAssetdataPath;
    private string jsonString;
    List<EnglishWord> danhSachTuVung;

    // Start is called before the first frame update
    void Start()
    {
        streamingAssetdataPath = Application.streamingAssetsPath + "/" + databaseName + ".json";
        LoadData();
    }

    private void LoadData()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            StartCoroutine(LoadDataAndroid());
        }
        else
        {
            jsonString = File.ReadAllText(streamingAssetdataPath);
            ProcessData();
        }
    }

    private IEnumerator LoadDataAndroid()
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
        danhSachTuVung = JsonConvert.DeserializeObject<List<EnglishWord>>(jsonString);
        //EnglishWord word = danhSachTuVung[1];

        Debug.Log(danhSachTuVung);
    }
}
