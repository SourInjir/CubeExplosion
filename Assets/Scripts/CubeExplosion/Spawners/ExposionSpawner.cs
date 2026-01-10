using UnityEngine;

public class ExplosionSpawner: ObjectSpawner
{
    private float _maxRadius = 100.0f;
    private float _maxForce = 100.0f;
    private const float ObjectLifeTime = 2.0f;

    public ExplosionSpawner(Pool pool, float radius, float force): base(pool)
    {
        _maxForce = force;
        _maxRadius = radius;
    }

    public void SpawnObject(Vector3 position, int generationCount = 1)
    {
        var explosion = _pool.Get();
        var explosionController = explosion.GetComponent<ExplosionObject>();
        explosionController.Initialize(PrepareDto(position, generationCount));
        _pool.ReturnWithDelay(explosion, ObjectLifeTime);
    }

    private ExplosionData PrepareDto(Vector3 position, int generationCount = 1)
    {
        return new ExplosionData
        {
            Position = position,
            Radius = _maxRadius,
            Force = _maxForce * generationCount,
        };
    }

}