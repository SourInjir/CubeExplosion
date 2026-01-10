using UnityEngine;
public class CubeSpawner: ObjectSpawner
{
    private const int MinSpawnCount = 2;
    private const int MaxSpawnCount = 6;
    private const float ScaleFactor = 0.5f;
    private const float ObjectLifeTime = 20.0f;

    private float _radius = 5f;
    private float _force = 100f;

    public CubeSpawner(CubePool pool, float radius, float force) : base(pool)
    {
        _radius = radius;
        _force = force;
    }

    public ClickableObjectData PrepareDto(Vector3 position, Vector3 scale)
    {
        return new ClickableObjectData
        {
            Position = position,
            Scale = scale * ScaleFactor,
            Color = new Color(Random.value, Random.value, Random.value),
        };
    }

    public GameObject SpawnObject(Vector3 position, Vector3 scale)
    {
        var obj = _pool.Get();
        obj.transform.position = position;
        obj.transform.localScale = scale * ScaleFactor;
        if (obj.TryGetComponent<IClickableObject>(out var clickableObject))
        {
            Renderer renderer = clickableObject.GetRenderer();

            if (renderer)
                renderer.material.color = new Color(Random.value, Random.value, Random.value);

            Rigidbody rigidbody = clickableObject.GetRigidbody();

            if (rigidbody)
                rigidbody.AddExplosionForce(_force, obj.transform.position, _radius);

            clickableObject.IncrementGenerationCount();
        }
        _pool.ReturnWithDelay(obj, ObjectLifeTime);
        return obj;
    }


    public void SpawnRandomQuantity(int min, int max, Vector3 position, Vector3 scale)
    {
        int count = Random.Range(MinSpawnCount, MaxSpawnCount);

        for (int i = 0; i < count; i++)
        {
            var objInstance = SpawnObject(position, scale);

            if (objInstance.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.AddExplosionForce(_force, objInstance.transform.position, _radius);
            }

        }

    }
}