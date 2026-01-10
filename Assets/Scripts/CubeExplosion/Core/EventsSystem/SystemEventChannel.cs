// CubeEventChannel.cs
using UnityEngine;
using System;

public class SystemEventChannel : MonoBehaviour
{
    public event Action<GameObject> ObjectClicked;
    public event Action LeftMouseClick;

    public event Action<GameObject> PlatformColide;

    public void DispatchEvent(GameObject obj)
    {
        ObjectClicked?.Invoke(obj);
    }

    public void DispatchMouseClickEvent()
    {
        LeftMouseClick?.Invoke();
    }

    public void DispatchPlatformColideEvent(GameObject obj)
    {
        PlatformColide?.Invoke(obj);
    }
}