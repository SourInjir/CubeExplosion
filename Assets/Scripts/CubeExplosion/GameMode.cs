using UnityEngine;

public class GameMode : MonoBehaviour
{
    private const float SpawnChanse = 100f;
    private const int MinSpawnCount = 2;
    private const int MaxSpawnCount = 6;

    [Header("Dependencies")]
    [SerializeField] private SystemEventChannel _systemEventChannel;
    [SerializeField] private CubePool _cubePool;
    [SerializeField] private ExposionPool _explosionPool;
    [SerializeField] private float _explosionRadius = 100f;
    [SerializeField] private float _explosionForce = 300f;

    [SerializeField] private float _spawnExplosionRadius = 5f;
    [SerializeField] private float _spawnExplosionForce = 100f;

    private CubeSpawner _cubeSpawner;
    private ExplosionSpawner _explosionSpawner;

    private void Awake()
    {
        _cubeSpawner = new CubeSpawner(_cubePool, _spawnExplosionRadius, _spawnExplosionForce);
        _explosionSpawner = new ExplosionSpawner(_explosionPool, _explosionRadius, _explosionForce);
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeToEvents();
    }

    private bool CanSpawn(int generationCount)
    {
        float chanse = Random.Range(0, SpawnChanse);
        float currentChanse = SpawnChanse / generationCount;
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

    private void HandleSpawnEvent(GameObject obj, int generationCount)
    {

        if (CanSpawn(generationCount) == true)
        {
            _cubeSpawner.SpawnRandomQuantity(MinSpawnCount, MaxSpawnCount, obj.transform.position, obj.transform.localScale);
        }
        else
        {
            _explosionSpawner.SpawnObject(obj.transform.position);
        }

        obj.SetActive(false);

    }
}