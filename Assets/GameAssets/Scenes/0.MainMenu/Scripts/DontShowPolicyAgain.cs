using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DontShowPolicyAgain : MonoBehaviour
{
    public GameObject privacyPolicyPanel;
    public void DontShowAgain()
    {
        /*PlayerPrefs.SetInt("privacyPolicyReaded", 1);
        PlayerPrefs.Save()*/;
        privacyPolicyPanel.SetActive(false);
    }
}
