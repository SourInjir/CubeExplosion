// GameManager.cs
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private SystemEventChannel _systemEventChannel;
    [SerializeField] private CubePool _cubePool;
    [SerializeField] private ExposionPool _explosionPool;
    [SerializeField] private CubeController _initialCube;
    [SerializeField] private ExplosionController _initialExplosion;

    private CubeSpawner _cubeSpawner;
    private ExplosionSpawner _explosionSpawner;

    private void Awake()
    {
        _cubeSpawner = new CubeSpawner(_cubePool, _systemEventChannel);
        _explosionSpawner = new ExplosionSpawner(_explosionPool, _systemEventChannel);
        InitCube();
    }
    private void OnDestroy()
    {
        _cubeSpawner?.Dispose();
        _explosionSpawner?.Dispose();
    }

    private void InitCube()
    {
        CubeData initialData = new CubeData
        {
            Position = Vector3.zero,
            Scale = Vector3.one,
            Color = Color.white,
            Generation = 0,
        };

        if (_initialCube == null)
        {
            GameObject spawnedObject = Instantiate(_cubePool.GetPrefab(), initialData.Position, new Quaternion());
            _initialCube = spawnedObject.GetComponent<CubeController>();
        }
        _initialCube.Initialize(initialData, false);
        _initialCube.SetEventChannel(_systemEventChannel);
    }
}