// GameManager.cs
using UnityEngine;
using System.Collections.Generic;

public class GameMode : MonoBehaviour
{
    private const float SpawnChanse = 100f;
    private const int MinSpawnCount = 2;
    private const int MaxSpawnCount = 6;
    private const float SpawnDelay = 1.0f;

    [Header("Dependencies")]
    [SerializeField] private SystemEventChannel _systemEventChannel;
    [SerializeField] private CubePool _cubePool;
    [SerializeField] private ExposionPool _explosionPool;
    [SerializeField] private float _explosionRadius = 100f;
    [SerializeField] private float _explosionForce = 300f;

    [SerializeField] private float _spawnExplosionRadius = 5f;
    [SerializeField] private float _spawnExplosionForce = 100f;

    private float spawnFactor = 1f;
   

    private CubeSpawner _cubeSpawner;
    private ExplosionSpawner _explosionSpawner;

    private void Awake()
    {
        _cubeSpawner = new CubeSpawner(_cubePool, _spawnExplosionRadius, _spawnExplosionForce);
        _explosionSpawner = new ExplosionSpawner(_explosionPool, _explosionRadius, _explosionForce);
        SubscribeToEvents();
    }

    private void Start()
    {
        StartCoroutine(CubeRain());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        UnsubscribeToEvents();
    }

    private void Update()
    {

    }

    private bool CanSpawn()
    {
        float chanse = Random.Range(0, SpawnChanse);
        float currentChanse = SpawnChanse / spawnFactor;
        return currentChanse >= chanse;
    }

    private void SubscribeToEvents()
    {
        _systemEventChannel.ObjectClicked += HandleSpawnEvent;
    }

    private void UnsubscribeToEvents()
    {
        _systemEventChannel.ObjectClicked -= HandleSpawnEvent;
    }

    private void HandleSpawnEvent(GameObject obj)
    {

        if (CanSpawn() == true)
        {
            _cubeSpawner.SpawnRandomQuantity(MinSpawnCount, MaxSpawnCount, obj.transform.position, obj.transform.localScale);
            spawnFactor *= 2;
        }
        else
        {
            _explosionSpawner.SpawnObject(obj.transform.position);
        }

        obj.SetActive(false);

    }

    protected System.Collections.IEnumerator CubeRain()
    {
        while (true)
        {
            _cubeSpawner.SpawnRandomQuantity(MinSpawnCount, MaxSpawnCount, new Vector3(0, 15, 0), new Vector3(1, 1, 1));
            yield return new WaitForSeconds(SpawnDelay);
        }
    }
}