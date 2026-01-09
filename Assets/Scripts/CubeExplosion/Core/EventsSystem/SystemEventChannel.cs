// CubeEventChannel.cs
using UnityEngine;
using System;

public class SystemEventChannel : MonoBehaviour
{
    public event Action<GameObject> ObjectClicked;
    public event Action LeftMouseClick;

    public void DispatchEvent(GameObject obj)
    {
        ObjectClicked?.Invoke(obj);
    }

    public void DispatchMouseClickEvent()
    {
        LeftMouseClick?.Invoke();
    }
}