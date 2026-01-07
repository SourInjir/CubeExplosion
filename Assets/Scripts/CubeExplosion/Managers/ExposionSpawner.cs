using UnityEngine;

public class ExplosionSpawner: ObjectSpawner
{
    private float _maxRadius = 100.0f;
    private float _maxForce = 100.0f;
    private const float ObjectLifeTime = 20.0f;
    public ExplosionSpawner(Pool pool, SystemEventChannel eventChannel): base(pool, eventChannel)
    {
    }

    public override void Dispose()
    {
        _eventChannel.ExplosionRequested -= HandleSpawnEvent;
    }
    protected override void SubscribeToEvents()
    {
        _eventChannel.ExplosionRequested += HandleSpawnEvent;
    }

    protected override void SpawnObject(Vector3 position)
    {
        var explosion = _pool.Get();
        var explosionController = explosion.GetComponent<ExplosionController>();
        explosionController.Initialize(PrepareDto(position));

        _pool.ReturnWithDelay(explosion, ObjectLifeTime);
    }

    private ExplosionData PrepareDto(Vector3 position)
    {
        return new ExplosionData
        {
            Position = position,
            Radius = _maxRadius,
            Force = _maxForce,
        };
    }

    private void HandleSpawnEvent(Vector3 position)
    {;
        SpawnObject(position);
    }

}