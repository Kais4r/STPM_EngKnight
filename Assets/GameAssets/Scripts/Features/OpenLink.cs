using UnityEngine;

public class OpenLink : MonoBehaviour
{
    public void OpenWebSiteFromLink(string url)
    {
        Application.OpenURL(url);
    }
}
