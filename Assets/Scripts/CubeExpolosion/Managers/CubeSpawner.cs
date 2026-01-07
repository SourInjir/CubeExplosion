using UnityEngine;
public class CubeSpawner: ObjectSpawner
{
    private const int MinSpawnCount = 2;
    private const int MaxSpawnCount = 6;
    private const float ScaleFactor = 0.5f;
    private const float ObjectLifeTime = 20.0f;
 
    public CubeSpawner(CubePool pool, SystemEventChannel eventChannel): base(pool, eventChannel)
    {
        
    }
    public override void Dispose()
    {
        _eventChannel.CubeClicked -= HandleSpawnEvent;
    }

    protected override void SubscribeToEvents()
    {
        _eventChannel.CubeClicked += HandleSpawnEvent;
    }

    private void HandleSpawnEvent(CubeData data)
    {
        int count = Random.Range(MinSpawnCount, MaxSpawnCount);

        for (int i = 0; i < count; i++)
        {
            SpawnObject(data);
        }
    }

    private CubeData PrepareDto(CubeData parentData)
    {
        return new CubeData
        {
            Position = CalculatePosition(parentData.Position),
            Scale = parentData.Scale * ScaleFactor,
            Color = new Color(Random.value, Random.value, Random.value),
            Generation = parentData.Generation + 1
        };
    }

    private void SpawnObject(CubeData parentData)
    {
        var cube = _pool.Get();
        var cubeController = cube.GetComponent<CubeController>();
        cubeController.Initialize(PrepareDto(parentData));

        _pool.ReturnWithDelay(cube, ObjectLifeTime);
    }

    private Vector3 CalculatePosition(Vector3 parentPos)
    {
        return parentPos;
    }
}