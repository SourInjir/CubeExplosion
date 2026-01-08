// CubePool.cs
using UnityEngine;

public class CubePool : Pool
{
    [SerializeField] private int _poolSize = 10;

    [SerializeField] private string _poolName = "CubeContainer";

    protected override string GetPoolName => _poolName;

    protected override int GetInitialPoolSize => _poolSize;

}