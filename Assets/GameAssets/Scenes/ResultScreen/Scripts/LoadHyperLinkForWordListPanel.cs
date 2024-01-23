using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadHyperLinkForWordListPanel : MonoBehaviour
{
    public string url = "";
    public void OpenLink()
    {
        Application.OpenURL(url);
    }
}
