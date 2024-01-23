using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowGeneratedWordList : MonoBehaviour
{
    private GameManagerSingleton _gameManagerSingleton;
    [SerializeField] GameObject englishWordPanelPrefab;
    [SerializeField] Transform scrollViewContent;

    public void Awake()
    {
        _gameManagerSingleton = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerSingleton>();
    }

    void Start()
    {
        foreach (EnglishWord englishWord in _gameManagerSingleton.generatedWords)
        {
            GameObject item = Instantiate(englishWordPanelPrefab);
            item.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = englishWord.WordName;
            item.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = englishWord.VietMeaning;
            item.GetComponent<LoadHyperLinkForWordListPanel>().url = englishWord.TranslationSource;
            item.transform.SetParent(scrollViewContent);
            item.transform.localScale = Vector2.one;
        }
    }
}
