using UnityEngine;
using System;

public class InputReader : MonoBehaviour
{
    private const int LeftMouseButton = 0;

    [SerializeField] private SystemEventChannel _eventChannel;

    public event Action MouseClicked;


    private void Update()
    {
        HandleMouseClick();
    }

    private void HandleMouseClick()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton) == false) 
            return;

        _eventChannel.DispatchMouseClickEvent();  
    }
}
