// CubeEventChannel.cs
using UnityEngine;
using UnityEngine.Events;

public class SystemEventChannel : MonoBehaviour
{
    public UnityAction<GameObject> ObjectClicked;
    public void DispatchEvent(GameObject obj)
    {
        ObjectClicked?.Invoke(obj);
    }
}