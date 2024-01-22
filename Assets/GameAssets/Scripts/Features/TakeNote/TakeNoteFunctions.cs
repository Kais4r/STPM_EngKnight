using System.IO;
using TMPro;
using UnityEngine;

public class TakeNoteFunctions : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    string filePath;
    
    public void saveNoteContent()
    {
        filePath = Path.Combine(Application.persistentDataPath, "NoteContent.txt");
        File.WriteAllText(filePath, _inputField.text);
    }
    public void clearNoteContent()
    {
        _inputField.text = "";
        filePath = Path.Combine(Application.persistentDataPath, "NoteContent.txt");
        File.WriteAllText(filePath, "");
    }
}
