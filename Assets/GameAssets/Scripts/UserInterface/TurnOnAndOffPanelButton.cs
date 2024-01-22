using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnAndOffPanelButton : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    //private bool _buttonState = false;

    public void TurnOnOrOffPanel()
    {
        _panel.SetActive(true);

        /*if (_buttonState == false)
        {
            _panel.SetActive( true );
            _buttonState = true;
        }
        else
        {
            _panel.SetActive(false);
            _buttonState = false;
        }*/
    }
}
