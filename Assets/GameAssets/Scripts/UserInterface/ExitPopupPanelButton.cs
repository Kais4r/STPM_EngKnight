using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPopupPanelButton : MonoBehaviour
{
    [SerializeField] private GameObject _popupPanel;

    public void ExitPanel()
    {
        _popupPanel.SetActive(false);
    }
}
