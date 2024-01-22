using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainMenuDataLoader : MonoBehaviour
{
    private GameManagerSingleton _gameManagerSingleton;
    string filePath;
    private string fileContent;

    private void Awake()
    {
        _gameManagerSingleton = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerSingleton>();
        // Read from file
        filePath = Path.Combine(Application.persistentDataPath, "NoteContent.txt");
        // Check if the file exists
        if (!File.Exists(filePath))
        {
            // Create the file and write content to it
            File.WriteAllText(filePath, "");
        }
        fileContent = File.ReadAllText(filePath);

        _gameManagerSingleton.noteContent = fileContent;

    }
    // Write to file
    /*string textToWrite = "Hello, World!";
    File.WriteAllText(filePath, textToWrite);*/


}
