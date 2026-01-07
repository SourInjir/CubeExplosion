// CubePool.cs
using UnityEngine;

public class ExposionPool : Pool
{
    [SerializeField] private int _poolSize = 10;

    [SerializeField] private string _poolName = "CubeConteiner";

    protected override string GetPoolName => _poolName;

    protected override int GetInitialPoolSize => _poolSize;

    [Header("Explosion Settings")]
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private float _explosionRadius = 3f;
    [SerializeField] private float _explosionForce = 50f;

    override protected GameObject CreateNewObject()
    {
        var obj = Instantiate(_explosionPrefab, _poolContainer);
        obj.SetActive(false);
        _pool.Enqueue(obj);
        return obj;
    }

    override public GameObject Get()
    {
        if (_pool.Count == 0)
            CreateNewObject();
        else
            Debug.Log("OLD OBJ");
        var obj = _pool.Dequeue();
        obj.SetActive(false);
        return obj;
    }
}