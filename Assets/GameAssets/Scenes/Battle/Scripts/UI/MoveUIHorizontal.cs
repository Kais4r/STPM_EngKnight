using UnityEngine;

public class MoveUIHorizontal : MonoBehaviour
{
    [SerializeField] private GameObject _userInterfaceObject;
    private RectTransform _rectTransform;
    [SerializeField] private int originalX, targetX;
    private int state = 0;
    private Vector2 newPos;


    private void Start()
    {
        _rectTransform = _userInterfaceObject.GetComponent<RectTransform>();
        newPos = _rectTransform.anchoredPosition;
    }

    public void MoveUIObjectHorizontal()
    {
        if (state == 0)
        {
            state = 1;
            newPos.x = targetX;
        }
        else
        {
            state = 0;
            newPos.x = originalX;
        }
        _rectTransform.anchoredPosition = newPos;
    }
}
