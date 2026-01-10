using UnityEngine;

public class ExposionPool : Pool
{
    [SerializeField] private int _poolSize = 10;
    [SerializeField] private string _poolName = "ExplosionContainer";

    protected override string GetPoolName => _poolName;
    protected override int GetInitialPoolSize => _poolSize;

    override protected GameObject CreateNewObject()
    {
        var obj = Instantiate(_objectPrefab, _poolContainer);
        obj.SetActive(false);
        _pool.Enqueue(obj);
        return obj;
    }

    override public GameObject Get()
    {

        if (_pool.Count == 0)
            CreateNewObject();

        var obj = _pool.Dequeue();
        obj.SetActive(false);
        return obj;
    }
}