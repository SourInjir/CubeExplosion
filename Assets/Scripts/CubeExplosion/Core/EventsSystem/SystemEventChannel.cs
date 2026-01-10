using UnityEngine;
using System;

public class SystemEventChannel : MonoBehaviour
{
    public event Action<GameObject, int> ObjectClicked;
    public event Action LeftMouseClick;

    public void DispatchEvent(GameObject obj, int generationCount)
    {
        ObjectClicked?.Invoke(obj, generationCount);
    }

    public void DispatchMouseClickEvent()
    {
        LeftMouseClick?.Invoke();
    }
}