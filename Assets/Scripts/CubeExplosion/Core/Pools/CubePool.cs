// CubePool.cs
using UnityEngine;

public class CubePool : Pool
{
    [SerializeField] private int _poolSize = 10;

    [SerializeField] private string _poolName = "CubeContainer";

    [SerializeField] private SystemEventChannel _eventChannel;

    protected override string GetPoolName => _poolName;

    protected override int GetInitialPoolSize => _poolSize;

    private void Awake()
    {
        base.Awake();
        _eventChannel.PlatformColide += PlatformColideHandler;
    }

    private void OnDestroy()
    {
        _eventChannel.PlatformColide -= PlatformColideHandler;
    }

    private void PlatformColideHandler(GameObject obj)
    {
        Debug.Log("RETURN TO POOL");
        ReturnWithDelay(obj, 3.0f);
    }
}