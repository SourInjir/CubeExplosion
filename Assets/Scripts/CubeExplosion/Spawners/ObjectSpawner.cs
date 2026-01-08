using UnityEngine;

public abstract class ObjectSpawner
{
    protected readonly Pool _pool;
    protected readonly SystemEventChannel _eventChannel;
 
    public ObjectSpawner(Pool pool)
    {
        _pool = pool;
    }

    public virtual void SpawnObject(Vector3 position)
    {

    }
}