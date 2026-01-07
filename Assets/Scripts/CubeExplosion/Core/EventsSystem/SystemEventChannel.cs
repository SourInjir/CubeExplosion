// CubeEventChannel.cs
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/SystemEventChannel")]
public class SystemEventChannel : ScriptableObject
{
    public UnityAction<CubeData> CubeClicked;
    public event UnityAction<Vector3> ExplosionRequested;
    public void DispatchEvent(CubeData data)
    {
        CubeClicked?.Invoke(data);
    }

    public void DispatchExplosionEvent(Vector3 position)
    {
        ExplosionRequested?.Invoke(position);
    }
}