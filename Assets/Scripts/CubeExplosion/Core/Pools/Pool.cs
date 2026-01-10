using System.Collections.Generic;
using UnityEngine;

public abstract class Pool : MonoBehaviour
{
    [SerializeField] protected GameObject _objectPrefab;
    protected abstract int GetInitialPoolSize { get; }
    protected abstract string GetPoolName { get; }
    
    protected Queue<GameObject> _pool = new Queue<GameObject>();
    protected Transform _poolContainer;

    public GameObject GetPrefab() => _objectPrefab;

    protected void Awake()
    {
        _poolContainer = new GameObject(GetPoolName).transform;
        _poolContainer.SetParent(transform);
        InitializePool();
    }

    protected void InitializePool()
    {

        for (int i = 0; i < GetInitialPoolSize; i++)
        {
            CreateNewObject();
        }

    }

    protected virtual GameObject CreateNewObject()
    {
        var cube = Instantiate(GetPrefab(), _poolContainer);
        cube.SetActive(false);
        _pool.Enqueue(cube);
        return cube;
    }

    virtual public GameObject Get()
    {
        if (_pool.Count == 0)
            CreateNewObject();

        var cube = _pool.Dequeue();
        cube.SetActive(true);
        return cube;
    }

    public void Return(GameObject cube)
    {
        cube.SetActive(false);
        cube.transform.SetParent(_poolContainer);
        _pool.Enqueue(cube);
    }

    public void ReturnWithDelay(GameObject obj, float delay)
    {
        StartCoroutine(ReturnCoroutine(obj, delay));
    }

    protected System.Collections.IEnumerator ReturnCoroutine(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Return(obj);
    }
}