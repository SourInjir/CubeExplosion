using UnityEngine;

public abstract class ObjectSpawner
{
    protected readonly Pool _pool;
    protected readonly SystemEventChannel _eventChannel;
 
    public ObjectSpawner(Pool pool, SystemEventChannel eventChannel)
    {
        _pool = pool;
        _eventChannel = eventChannel;
        SubscribeToEvents();
    }

    public virtual void Dispose()
    {

    }

    protected virtual void SubscribeToEvents()
    {

    }

    protected virtual void SpawnObject(Vector3 position)
    {
    
    }
}