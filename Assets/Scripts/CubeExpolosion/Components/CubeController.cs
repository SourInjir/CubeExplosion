// CubeController.cs
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class CubeController : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private SystemEventChannel _eventChannel;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _explosionForce = 50f;
    private float spawnFactor = 2f;
    private const float SpawnChanse = 100f;

    private CubeData _cubeData;
    public void SetEventChannel(SystemEventChannel channel) => _eventChannel = channel;

    private void OnMouseDown()
    {
        if (CanSpawnCubes())
        {
            _cubeData.Position = transform.position;
            _eventChannel.DispatchEvent(_cubeData);
        }
        else
        {
            _eventChannel.DispatchExplosionEvent(transform.position);
        }
        gameObject.SetActive(false);
    }

    private bool CanSpawnCubes()
    {
        float chanse = Random.Range(0, SpawnChanse);
        float currentChanse = SpawnChanse / (_cubeData.Generation * spawnFactor);
        return currentChanse >= chanse;
    }

    public void Initialize(CubeData data, bool useForce = true)
    {
        _cubeData = data;
        ApplyData(useForce);
    }

    private void ApplyData(bool useForce = true)
    {
        transform.position = _cubeData.Position;
        transform.localScale = _cubeData.Scale;
        _renderer.material.color = new Color(Random.value, Random.value, Random.value);
        if (useForce)
            _rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }
}