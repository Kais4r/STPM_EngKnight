using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Database : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _questionContentText;

    string jsonString = @"[
    {
        'Id': '0',
        'WordName': 'home',
        'EnglishDescription': 'place you stay at when you have money',
        'VietMeaning': 'ngôi nhà',
        'VietDescription': 'nơi bạn ở khi bạn không thất nghiệp'
    },
    {
        'Id': '1',
        'WordName': 'computer',
        'EnglishDescription': 'electrical devices that has a physical keyboard, a mouse and a screen that you use for studying and gaming',
        'VietMeaning': 'máy tính',
        'VietDescription': 'thiết bị điện tử có chuột, bàn phím, màn hình, dùng để học tập, làm việc và chơi game.'
    }
    ]";



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.persistentDataPath);

        List<EnglishWord> danhSachTuVung = JsonConvert.DeserializeObject<List<EnglishWord>>(jsonString);
        //Console.WriteLine(danhSachTuVung.Count);
        // 2

        EnglishWord word = danhSachTuVung[1];
        _questionContentText.text = Application.persistentDataPath;

        //Debug.Log(word.WordName + ": " + word.VietDescription);
        // Product 1
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
}
